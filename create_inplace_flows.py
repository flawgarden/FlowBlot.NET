import os
import re
import common_flowblot_info as info
import flowblot_tools as tools


# creates flow's copy, adding source and sink functions along with it
def create_inplace_copy(flow_path):
    flow_file_name = flow_path.split(os.path.sep)[-1]
    flow_class_name = flow_file_name[:-3]
    flow_class_inplace = flow_class_name

    substitutions = [[info.SINK_CALL_REGEX,
                      flow_class_inplace + "." + info.INPLACE_SINK_CALL],
                     [info.SOURCE_CALL_REGEX,
                      flow_class_inplace + "." + info.INPLACE_SOURCE_CALL],
                     [info.FLOW_NAME_REGEX, info.INPLACE_FLOW_NAME],
                     [info.NAMESPACE_REGEX, info.INPLACE_NAMESPACE]]

    new_file_contents = []
    with open(flow_path) as file:
        insert_sink_source = False
        while line := file.readline():
            if not line:
                continue

            if insert_sink_source:
                line += "{0}\n\n{1}\n\n" \
                    .format(info.INPLACE_SINK_CALL_TEMPLATE,
                            info.INPLACE_SOURCE_CALL_TEMPLATE)
                insert_sink_source = False

            if bool(re.match(info.FLOW_CLASS_REGEX, line)):
                insert_sink_source = True

            for regex, replace in substitutions:
                line = re.sub(regex, replace, line)

            new_file_contents.append(line)

    inplace_path = re.sub(info.FLOW_NAME_REGEX,
                          info.INPLACE_FLOW_NAME,
                          flow_path)
    with open(inplace_path, "w") as new_file:
        new_file.write("".join(new_file_contents))


# rewriting functions that are already defined in file
def rewrite_source_and_sink(file_path):
    with open(file_path, "r") as file:
        contents = file.readlines()

    insert = None
    ignore = False
    new_contents = []
    for line in contents:
        if "Source" in line:
            insert = info.INPLACE_SOURCE_BODY
        if "Sink" in line:
            insert = info.INPLACE_SINK_BODY

        if "}" in line and ignore:
            ignore = False

        if ignore:
            continue
        new_contents.append(line)

        if "{" in line and insert:
            ignore = True
            new_contents.append(insert + "\n")
            insert = None

    with open(file_path, "w") as file:
        file.write("".join(new_contents))


def insert_inplace_flows(csproj_path):
    def insert_inplace(line):
        # removing duplicates in case this was already run before
        if tools.does_contain_regex(info.FLOW_INPLACE_NAME_REGEX, line):
            return ""

        if tools.does_contain_regex(info.FLOW_NAME_REGEX, line):
            add_line = re.sub(info.FLOW_NAME_REGEX,
                              info.INPLACE_FLOW_NAME,
                              line)
            return line + add_line

        return line

    tools.modify_file_per_line(csproj_path, insert_inplace)


def main():
    flows = tools.get_files_by_regex("." + os.path.sep, info.FLOW_FILE_NAME_REGEX)

    for flow in flows:
        create_inplace_copy(flow)

    insert_inplace_flows(os.path.join("src", "FlowBlot.csproj"))
    insert_inplace_flows(os.path.join("src", "Program.cs"))
    rewrite_source_and_sink(os.path.join("src", "Model", "Framework.cs"))


if __name__ == "__main__":
    main()
