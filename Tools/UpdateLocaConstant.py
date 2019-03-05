import csv

def loadCsv(output):
    csv_file = open("../CyberStories/Assets/GlobalManager/LocalizationManager/loca.csv", "r")
    csv_reader = csv.DictReader(csv_file, delimiter=';')
    line_count = 1
    for row in csv_reader:
        output.write("\tpublic const uint " + row.get('Variable Name') + " = " + str(line_count) + ";\n")
        line_count += 1
    csv_file.close()


def main():
    output = open("../CyberStories/Assets/GlobalManager/LocalizationManager/LocaConst.cs", "w")
    output.write("public static class LocaConst\n")
    output.write("{\n")
    loadCsv(output)
    output.write("}\n")
    output.close()


main()
