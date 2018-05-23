import shutil
import os

from configuration_global.config_reader import ConfigReader
from configuration_global.exception_handler import exception


class DirectoryManager():
    def __init__(self):
        self.config = ConfigReader()

    # def get_positive_training_data(self):
    #     image_paths = [path.join(self.config.positive_data_path, f) for f in listdir(self.config.positive_data_path)]
    #     return image_paths
    #
    # def get_negative_training_data(self):
    #     image_paths = [path.join(self.config.negative_data_path, f) for f in listdir(self.config.negative_data_path)]
    #     return image_paths

    def get_files_from_directory(self, source_path):
        image_paths = [os.path.join(source_path, f) for f in os.listdir(source_path)]
        return image_paths

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
