import shutil
import os

from configuration_global.config_reader import ConfigReader
from configuration_global.exception_handler import exception
from fnmatch import fnmatch


class DirectoryManager():
    def __init__(self):
        self.config = ConfigReader()

    def get_files_from_directory(self, source_path):
        image_paths = [os.path.join(source_path, f) for f in os.listdir(source_path)]
        return image_paths

    def get_all_images_from_all_subdirectories(self, source_path):
        pattern = ("jpg", "png", "jpeg", "bmp")
        result = []
        for path, subdirs, files in os.walk(source_path):
            for name in files:
                if name.lower().endswith(pattern):
                    result.append(os.path.join(path, name))
        return result

    def get_filenames_from_directory(self, directory):
        files = [f for f in os.listdir(directory)]
        return files

    @exception
    def create_directory_if_doesnt_exist(self, directory):
        if not os.path.exists(directory):
            os.makedirs(directory)

    @exception
    def clean_directory(self, directory):
        if os.path.exists(directory):
            shutil.rmtree(directory)
