import json
import os


def conv(json_obj, line_padding=""):
    result_list = list()

    json_obj_type = type(json_obj)

    if json_obj_type is list:
        for sub_elem in json_obj:
            result_list.append(conv(sub_elem, line_padding))

        return "\n".join(result_list)

    if json_obj_type is dict:
        for tag_name in json_obj:
            sub_obj = json_obj[tag_name]
            result_list.append("%s<%s>" % (line_padding, tag_name))

            if tag_name == "data2D":
                result_list.append(conv(sub_obj, "" + ","))
            else:
                result_list.append(conv(sub_obj, "\t" + line_padding))
            result_list.append("%s</%s>" % (line_padding, tag_name))

        return "\n".join(result_list)

    return "%s%s" % (line_padding, json_obj)


def readtxt(filepth):
    fl = open(filepth)
    return fl.read()


os.chdir(r"C:\Users\sbani\Desktop\Falingunit\Extraterrestrial\Content\Maps")

for file in os.listdir():
    content = ""
    if file.endswith(".json"):
        file_path = fr"{file}"
        content = readtxt(file_path)
        if os.path.isfile(f'{file.removesuffix(".json")}.xml'):
            os.remove(f'{file.removesuffix(".json")}.xml')
        f = open(rf'{file.removesuffix(".json")}.xml', 'x')
        f.write((conv(json.loads(content))))
