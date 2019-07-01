from configuration_global.config_reader import ConfigReader
from configuration_global.logger_factory import LoggerFactory
from keras.preprocessing.image import ImageDataGenerator


class DataProvider():
    def __init__(self):
        self.logger = LoggerFactory()
        self.config = ConfigReader()
        self.dataPath = self.config.training_data_path
        self.testDataPath = self.config.test_data_path

    def get_training_data_set(self):
        train_data_generator = ImageDataGenerator(rescale=1. / 255,
                                                  shear_range=0.2,
                                                  zoom_range=0.2,
                                                  horizontal_flip=True)
        training_set = train_data_generator.flow_from_directory(self.dataPath,
                                                                target_size=(24, 24),
                                                                batch_size=32,
                                                                class_mode='binary')
        return training_set

    def get_test_data_set(self):
        test_data_generator = ImageDataGenerator(rescale=1. / 255)
        test_set = test_data_generator.flow_from_directory(self.testDataPath,
                                                           target_size=(24, 24),
                                                           batch_size=32,
                                                           class_mode='binary')
        return test_set
