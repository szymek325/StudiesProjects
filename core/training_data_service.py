import cv2
import os

from configuration_global.config_reader import ConfigReader
from configuration_global.exception_handler import exception
from configuration_global.logger_factory import LoggerFactory
from core.ImageConverter import ImageConverter
from core.directory_manager import DirectoryManager


class TrainingDataService():
    def __init__(self):
        self.config = ConfigReader()
        self.logger = LoggerFactory()
        self.directory = DirectoryManager()
        self.imageConverter = ImageConverter()
    @exception
    def prepare_training_data(self):
        self.logger.info("preparing training data")
        self.prepare_positive_data()

    def prepare_positive_data(self):
        self.logger.info("preparing POSITIVE DATA")
        self.__create_binary_files_from_source_location__(self.config.positive_data_path,
                                                          self.config.positive_binary_data_path)

    def prepare_negative_data(self):
        self.logger.info("preparing POSITIVE DATA")
        self.__create_binary_files_from_source_location__(self.config.negative_data_path,
                                                          self.config.negative_binary_data_path)

    def __create_binary_files_from_source_location__(self, source_path, output_directory):
        self.logger.info(f"work on directory {source_path} started")
        files_to_process = self.directory.get_filenames_from_directory(source_path)
        for file in files_to_process:
            binary_file, file_name = self.imageConverter.convert_to_binary(file)
            file_location = os.path.join(output_directory, file_name)
            cv2.imwrite(file_location, binary_file)
