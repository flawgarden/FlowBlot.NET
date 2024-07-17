import os
import re


def get_files_by_regex(cur_path, regex):
    flows = []
    for entry in os.scandir(cur_path):
        if entry.is_dir():
            inner_flows = get_files_by_regex(entry.path, regex)
            flows.extend(inner_flows)
        elif entry.is_file() and bool(re.match(regex, entry.name)):
            flows.append(entry.path)
    return flows


def modify_file_per_line(file_path, func_line_changer):
    with open(file_path, "r") as file:
        new_contents = list(map(func_line_changer, file.readlines()))
    with open(file_path, "w") as file:
        file.write("".join(new_contents))


def does_contain_regex(regex, string):
    return len(re.findall(regex, string)) > 0
