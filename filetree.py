import os

def write_file_tree(directory, output_file, indent=""):
    """
    Recursively writes the file tree of a directory to a file.

    Args:
        directory (str): The path to the directory.
        output_file: The file object to write the output to.
        indent (str): Indentation for the current directory level.
    """
    try:
        entries = os.listdir(directory)
        for entry in entries:
            path = os.path.join(directory, entry)
            if os.path.isdir(path):
                output_file.write(f"{indent}📁 {entry}/\n")
                write_file_tree(path, output_file, indent + "    ")
            else:
                output_file.write(f"{indent}📄 {entry}\n")
    except PermissionError:
        output_file.write(f"{indent}❌ [Permission Denied]\n")
    except FileNotFoundError:
        output_file.write(f"{indent}❌ [Not Found]\n")

# Replace this with the actual directory path
base_directory = r"D:\University_MSc\1_Felev\szoftverarchitekturak\hazi\v1\application-localization"

if __name__ == "__main__":
    output_file_path = "file_tree.txt"
    with open(output_file_path, "w", encoding="utf-8") as output_file:
        output_file.write(f"File tree for: {base_directory}\n")
        write_file_tree(base_directory, output_file)
    
    print(f"File tree has been written to {output_file_path}")
