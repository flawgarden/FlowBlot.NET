import shutil
import os
import toml
import common_flowblot_info as info
import flowblot_tools as tools


def add_indent_to_code(code):
    new_code = code.split("\n")
    new_code = list(map(lambda x: info.INPLACE_CODE_INDENT + x, new_code))
    new_code = "\n".join(new_code)
    new_code += "\n"
    return new_code


def fill_flow_template(source_code, sink_code, flow_path):
    def insert_code(line):
        if info.INPLACE_SINK_MARK in line:
            return add_indent_to_code(sink_code)
        if info.INPLACE_SOURCE_MARK in line:
            return add_indent_to_code(source_code)
        return line

    with open(flow_path, "r") as file:
        contents = file.readlines()
    new_contents = list(map(insert_code, contents))

    with open(flow_path, "w") as file:
        # writing includes with the rest of the file
        content = "{0}\n{1}" \
                .format(info.INPLACE_CODE_USING,
                        "".join(new_contents)[3:])
        file.write(content)


def create_flowblot(source, sink, template_path, rewrite_if_exists=True):
    new_flowblot_name = source["name"] + "2" + sink["name"] + "_CWE" + str(sink["cwe"])
    if os.path.exists(new_flowblot_name):
        if not rewrite_if_exists:
            return
        shutil.rmtree(new_flowblot_name)
    shutil.copytree(template_path, os.path.join(".", new_flowblot_name))

    # searching for inplace flows where sink and source must be specified explicitly
    flows = tools.get_files_by_regex(os.path.join(".", new_flowblot_name),
                                     info.FLOW_INPLACE_NAME_REGEX + r"\.cs")
    # filling the framework's sink and source as well
    flows.append(os.path.join(".", new_flowblot_name, "Model", "Framework.cs"))
    for flow in flows:
        fill_flow_template(source["code"], sink["code"], flow)


def main():
    with open("raw_parts.toml", "r") as parts_file:
        parts_data = toml.load(parts_file)
        sources = parts_data["sources"]
        sinks = parts_data["sinks"]

    for source in sources:
        for sink in sinks:
            create_flowblot(source, sink, r"src")


if __name__ == "__main__":
    main()
