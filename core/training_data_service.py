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
        self.temporaryFilesDirectory = self.config.temporary_files_path

    @exception
    def prepare_training_data(self):
        self.logger.info("preparing training data")
        self.prepare_positive_data()
        self.prepare_negative_data()

    def prepare_positive_data(self):
        self.logger.info("preparing POSITIVE DATA")
        self.directory.create_directory_if_doesnt_exist(self.temporaryFilesDirectory)
        self.__create_binary_files_from_source_location__(self.config.positive_data_path,
                                                          self.config.positive_binary_data_path)
        self.directory.clean_directory(self.temporaryFilesDirectory)

    def prepare_negative_data(self):
        self.logger.info("preparing POSITIVE DATA")
        self.directory.create_directory_if_doesnt_exist(self.temporaryFilesDirectory)
        self.__create_binary_files_from_source_location__(self.config.negative_data_path,
                                                          self.config.negative_binary_data_path)
        self.directory.clean_directory(self.temporaryFilesDirectory)

    def __create_binary_files_from_source_location__(self, source_path, output_directory):
        self.logger.info(f"work on directory {source_path} started")
        self.directory.create_directory_if_doesnt_exist(output_directory)
        files_to_process = self.directory.get_files_from_directory(source_path)
        for file in files_to_process:
            binary_file, file_name = self.imageConverter.convert_to_binary(file)
            file_location = os.path.join(output_directory, file_name)
            cv2.imwrite(file_location, binary_file)
        self.logger.info(f"work on directory {source_path} FINISHED "
                         f"\n {len(files_to_process)} processed")
