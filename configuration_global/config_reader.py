import json
import os


class ConfigReader:

    def __init__(self):
        self.dir_path = os.path.dirname(os.path.realpath(__file__))
        self.project_directory = os.path.abspath(os.path.join(self.dir_path, ".."))
        self.configuration = json.load(open(f"{self.dir_path}/config.json"))

    @property
    def positive_data_path(self):
        return os.path.join(self.project_directory, self.configuration["positive_data_path"])

    @property
    def negative_data_path(self):
        return os.path.join(self.project_directory, self.configuration["negative_data_path"])

    @property
    def positive_binary_data_path(self):
        return os.path.join(self.project_directory, self.configuration["positive_binary_data_path"])

    @property
    def negative_binary_data_path(self):
        return os.path.join(self.project_directory, self.configuration["negative_binary_data_path"])

    @property
    def logs_path(self):
        return os.path.join(self.project_directory, self.configuration["logs_path"])
