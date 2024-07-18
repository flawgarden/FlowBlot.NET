#!/usr/bin/env python3

import os
import re
import subprocess
import time

UPDATE_MARK = "updated"
PROJECT_END = "</Project>"


def find_all_files_in_dir_nr(directory):
    files = os.listdir(directory)

    # append base dir
    files = list(map(lambda x: os.path.join(directory, x), files))

    # make sure each path is a file (and not a directory)
    files = list(filter(lambda x: os.path.isfile(x), files))

    return files


def open_file_and_get_contents(file):
    with open(file, 'r') as f:
        try:
            content = f.read()
            return content
        except UnicodeDecodeError as error:
            print("\n\n")
            print(error)
            print("Weird char in ", file)
            print("\n")
            return None


def write_file(filename, contents):
    with open(filename, 'w') as f:
        f.write(contents)


def print_with_timestamp(contents):
    print("[" + time.ctime(None) + "] " + contents)


def is_buildable(project_path):
    return subprocess.run(["dotnet", "build", project_path],
                          stdout=subprocess.DEVNULL).returncode == 0


# runs upgrading script provided by Microsoft
# see: https://learn.microsoft.com/en-us/dotnet/core/porting/upgrade-assistant-overview
def run_upgrade_assistant(csproj):
    try:
        output = subprocess.check_output(" ".join(["upgrade-assistant",
                                                   "upgrade",
                                                   csproj,
                                                   "--operation Inplace",
                                                   "--targetFramework net6.0",
                                                   "--non-interactive"]),
                                         shell=True).decode("latin-1")

        # verifying it finished with success by looking at the output
        # (process may fail but return zero nonetheless)
        success = "Succeeded" in output.split("\n")[-3]

    # catching any error happening within the process
    except Exception as e:
        print_with_timestamp(
            "Exception within 'run_upgrade_assistant':\n" + str(e))
        return False

    return success


def filter_by_extension(files, ext):
    return list(filter(lambda x: x.endswith("." + ext), files))


# resolves several update issues with moved standard libraries etc
def update_code_contents(file_path):
    contents = open_file_and_get_contents(file_path)

    # library SqlClient was moved
    contents = contents.replace(" System.Data.SqlClient",
                                " Microsoft.Data.SqlClient")

    # File AccessControl is now specified differently
    contents = re.sub(r"File\.Create\((.*?), (.*?), (.*?), fSecurity\);",
                      r"File.Create(\1, \2, \3).SetAccessControl(fSecurity);",
                      contents)

    contents = re.sub(r"File\.GetAccessControl\((.*?)\)",
                      r"(new FileInfo(\1).GetAccessControl())",
                      contents)

    write_file(file_path, contents)


DECLARATIONS_TO_REMOVE = [r'(?s)<Reference Include="NLog">.*?</Reference>',
                          r'<PackageReference Include="NLog".*?/>',
                          '<Reference Include="netstandard" />',
                          r'<PackageReference Include="System.Data.DataSetExtensions".*?/>']

# spaces are used to keep up with the existing .csproj codestyle
INCLUDE_DECLARATIONS = "\n".join(["",
                                  "  <PropertyGroup>",
                                  "    <NoWarn>0162,SYSLIB0011,SYSLIB0014</NoWarn>",
                                  "  </PropertyGroup>",
                                  "  <ItemGroup>",
                                  '    <PackageReference Include="System.CodeDom" Version="8.0.0" />',
                                  '    <PackageReference Include="Microsoft.Data.SqlClient" Version="5.2.1" />',
                                  "  </ItemGroup>",
                                  PROJECT_END])


def update_project_contents(file_path):
    contents = open_file_and_get_contents(file_path)

    for regex in DECLARATIONS_TO_REMOVE:
        contents = re.sub(regex, "", contents)

    contents = re.sub(PROJECT_END, INCLUDE_DECLARATIONS, contents)

    write_file(file_path, contents)


def update_project_in_cwd(project_name):
    project_files = find_all_files_in_dir_nr(".")

    # checking if the current project has already been updated
    if len(filter_by_extension(project_files, UPDATE_MARK)) > 0:
        return True

    csproj_file = filter_by_extension(project_files, "csproj")[0]
    cs_files = filter_by_extension(project_files, "cs")

    if not run_upgrade_assistant(csproj_file):
        print_with_timestamp(
            "upgrade-assistant failed for: " + csproj_file + "!\n")
        return False

    for file in cs_files:
        update_code_contents(file)
    update_project_contents(csproj_file)

    if not is_buildable(""):
        print_with_timestamp(
            "dotnet build failed for: " + project_name + "!\n")
        return False

    # marking the project directory as updated
    write_file("." + UPDATE_MARK, "")

    return True


def get_list_of_cwe_projects():
    cwe_regex = "CWE"
    testcases_path = os.path.join('src', 'testcases')
    cwes = []

    # get the CWE directories in testcases folder
    cwe_dirs = os.listdir(testcases_path)
    cwe_dirs = map(lambda x: os.path.join(testcases_path, x), cwe_dirs)

    # only allow directories
    cwe_dirs = filter(lambda x: os.path.isdir(x) and cwe_regex in x, cwe_dirs)

    for cwe_dir in cwe_dirs:
        cwe_sub_dirs = os.listdir(cwe_dir)

        # check if the CWE is split into subdirectories
        if 's01' in cwe_sub_dirs:
            for sub_dir in cwe_sub_dirs:
                cwes.append(os.path.join(cwe_dir, sub_dir))
        else:
            cwes.append(cwe_dir)

    return cwes


if __name__ == "__main__":
    root = os.getcwd()

    cwe_projects = get_list_of_cwe_projects()
    cwe_projects.append("src")

    for proj in cwe_projects:

        # changing directory so the file paths appear shorter
        os.chdir(proj)

        if not update_project_in_cwd(proj):
            print_with_timestamp(
                "Could not update " + proj + "!\naborting...")
            exit(2)

        os.chdir(root)

        print_with_timestamp("Successfully updated " + proj + "!\n")
