#!/usr/bin/env python3

import subprocess
import os
import platform

match platform.system():
    case "Linux":
        dotnet_build = ["xbuild"]
    case "Windows":
        dotnet_build = ["dotnet", "build"]
    case _:
        print("Unsupported platform!")
        exit(1)


def is_buildable():
    return subprocess.run(dotnet_build,
                          stdout=subprocess.DEVNULL).returncode == 0


def get_projects(cur_path):
    projs = []
    for entry in os.scandir(cur_path):
        if entry.is_dir():
            inner_projs = get_projects(entry.path)
            projs.extend(inner_projs)
        elif entry.is_file() and entry.name.endswith(".csproj"):
            projs.append(entry)
    return projs


if __name__ == "__main__":
    root = os.getcwd()
    for proj in get_projects(root):
        os.chdir(proj.path[:-len(proj.name) - 1])
        if not is_buildable():
            print(proj.path[len(root) + 1:] + " failed to build!")
            exit(-1)
        os.chdir(root)
