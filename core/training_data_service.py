from configuration_global.config_reader import ConfigReader
from configuration_global.logger_factory import LoggerFactory
from core.directory_manager import DirectoryManager


class TrainingDataService():
    def __init__(self):
        self.config=ConfigReader()
        self.logger=LoggerFactory()
        self.directory=DirectoryManager()

    def prepare_training_data(self):
        self.logger.info("preparing training data")


    
