SINK_CALL_MARK_BEGIN = r".*?\/\*.*?:Flow_.*?codethreat.*?"
SINK_CALL_MARK_END = r".*?\*\/"

SINK_CALL_REGEX = r"FlowBlot\.Model\.Framework\.Sink\((.*?)\)"
INPLACE_SINK_CALL = r"Sink(\1)"

SOURCE_CALL_REGEX = r"FlowBlot\.Model\.Framework\.Source\(\)"
INPLACE_SOURCE_CALL = r"Source()"

NAMESPACE_REGEX = r"FlowBlot\.Flows"
INPLACE_NAMESPACE = r"FlowBlot.Flows_inplace"

FLOW_NAME_REGEX = r"Flow_(\d+)"
INPLACE_FLOW_NAME = r"Flow_\1_inplace"

FLOW_INPLACE_NAME_REGEX = FLOW_NAME_REGEX + "_inplace"
FLOW_FILE_NAME_REGEX = FLOW_NAME_REGEX + r"\.cs"

FLOW_CLASS_REGEX = r"\s+public class " + FLOW_NAME_REGEX

INPLACE_SINK_MARK = r"/* SINK GENERATED CODE */"

INPLACE_SINK_BODY = "\t\t\t" + INPLACE_SINK_MARK

INPLACE_SINK_CALL_TEMPLATE = "\n".join((
    "\t\tpublic static void Sink(string input)",
    "\t\t{",
    INPLACE_SINK_BODY,
    "\t\t}"
))

INPLACE_SOURCE_MARK = r"/* SOURCE GENERATED CODE */"

INPLACE_SOURCE_BODY = "\n".join((
    "\t\t\tstring data = string.Empty;",
    "\t\t\t" + INPLACE_SOURCE_MARK,
    "\t\t\treturn data;",
))

INPLACE_SOURCE_CALL_TEMPLATE = "\n".join((
    "\t\tprivate static string Source()",
    "\t\t{",
    INPLACE_SOURCE_BODY,
    "\t\t}"
))

INPLACE_CODE_INDENT = "\t\t"

INPLACE_CODE_USING = """using System;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Web;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
"""
