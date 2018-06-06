import json
import os
import platform


class ConfigReader:

    def __init__(self):
        self.dir_path = os.path.dirname(os.path.realpath(__file__))
        self.project_directory = os.path.abspath(os.path.join(self.dir_path, ".."))
        self.configuration = json.load(open(f"{self.dir_path}/config.json"))

    @property
    def overwrite_old_nn(self):
        return self.configuration["overwrite_old_nn"]

    @property
    def neural_networks_path(self):
        path = self.configuration["neural_networks_path"]
        return self.__modify_accordingly_to_operating_system__(path)

    @property
    def training_data_path(self):
        path = self.configuration["training_data_path"]
        return self.__modify_accordingly_to_operating_system__(path)

    @property
    def test_data_path(self):
        path = self.configuration["test_data_path"]
        return self.__modify_accordingly_to_operating_system__(path)

    @property
    def positive_training_data_path(self):
        path = self.configuration["positive_training_data_path"]
        return self.__modify_accordingly_to_operating_system__(path)

    @property
    def negative_training_data_path(self):
        path = self.configuration["negative_training_data_path"]
        return self.__modify_accordingly_to_operating_system__(path)

    @property
    def positive_testing_data_path(self):
        path = self.configuration["positive_testing_data_path"]
        return self.__modify_accordingly_to_operating_system__(path)

    @property
    def negative_testing_data_path(self):
        path = self.configuration["negative_testing_data_path"]
        return self.__modify_accordingly_to_operating_system__(path)

    @property
    def logs_path(self):
        path = self.configuration["logs_path"]
        return self.__modify_accordingly_to_operating_system__(path)

    @property
    def temporary_files_path(self):
        path = self.configuration["temporary_files_path"]
        return self.__modify_accordingly_to_operating_system__(path)

    @property
    def pre_trained_neural_network_name(self):
        return self.configuration["pre_trained_neural_network_name"]

    def __modify_accordingly_to_operating_system__(self, path):
        if platform.system() == "Windows":
            path = os.path.join(self.project_directory, path)
        else:
            path = './' + path
        return path
