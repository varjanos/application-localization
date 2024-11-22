import os

def print_file_tree(start_path, output_file=None):
    """
    Kiírja egy mappa tartalmát fájlfa formátumban a konzolra és/vagy egy fájlba.
    
    :param start_path: Az induló mappa elérési útvonala.
    :param output_file: Ha meg van adva, ide menti az eredményt.
    """
    result = []
    for root, dirs, files in os.walk(start_path):
        level = root.replace(start_path, "").count(os.sep)
        indent = " " * 4 * level
        result.append(f"{indent}{os.path.basename(root)}/")
        sub_indent = " " * 4 * (level + 1)
        for file in files:
            result.append(f"{sub_indent}{file}")
    
    # Konzolra kiírás
    print("\n".join(result))
    
    # Eredmény mentése fájlba (opcionális)
    if output_file:
        with open(output_file, "w", encoding="utf-8") as file:
            file.write("\n".join(result))
        print(f"\nA mappastruktúrát elmentettük ide: {output_file}")

# Mappa elérési útja (itt az application_localization)
folder_path = r"D:\University_MSc\1_Felev\szoftverarchitekturak\project\application-localization"

output_file = "file_tree.txt"  # Opcionális, az eredmény ide kerül

# Kód futtatása
print_file_tree(folder_path, output_file)
