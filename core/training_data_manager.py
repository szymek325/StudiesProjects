from configuration_global.config_reader import ConfigReader
from configuration_global.logger_factory import LoggerFactory
from keras.preprocessing.image import ImageDataGenerator


class TrainingDataManager():
    def __init__(self):
        self.logger = LoggerFactory()
        self.config = ConfigReader()
        self.dataPath = self.config.training_data_path
        self.testDataPath= self.config.test_data_path

    def get_training_data(self):
        train_datagen = ImageDataGenerator(rescale=1. / 255,
                                           shear_range=0.2,
                                           zoom_range=0.2,
                                           horizontal_flip=True)
        training_set = train_datagen.flow_from_directory('dataset/training_set',
                                                         target_size=(64, 64),
                                                         batch_size=32,
                                                         class_mode='binary')
        return training_set

    def __create__image_data_geenerator(self):
        test_datagen = ImageDataGenerator(rescale=1. / 255)

        test_set = test_datagen.flow_from_directory('dataset/test_set',
                                                    target_size=(64, 64),
                                                    batch_size=32,
                                                    class_mode='binary')
        return test_set
