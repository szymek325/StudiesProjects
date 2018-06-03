import classifier as classifier
from keras.models import Sequential
from keras.layers import Conv2D
from keras.layers import MaxPooling2D
from keras.layers import Flatten
from keras.layers import Dense
import numpy as np
import sys
import os.path
from os import listdir
from keras.preprocessing import image

tmg_path = str(sys.argv[1])

classifier = Sequential()

classifier.add(Conv2D(32, (3, 3), input_shape=(64, 64, 3), activation='relu'))

classifier.add(MaxPooling2D(pool_size=(2, 2)))

classifier.add(Conv2D(32, (3, 3), activation='relu'))

classifier.add(MaxPooling2D(pool_size=(2, 2)))

classifier.add(Flatten())

classifier.add(Dense(units=128, activation='relu'))

classifier.add(Dense(units=1, activation='softmax'))

classifier.compile(optimizer='rmsprop', loss='categorical_crossentropy', metrics=['accuracy'])

# fitting the cnn to images
from keras.preprocessing.image import ImageDataGenerator

train_datagen = ImageDataGenerator(
    rescale=1. / 255,
    shear_range=0.2,
    zoom_range=0.2,
    horizontal_flip=True)

test_datagen = ImageDataGenerator(rescale=1. / 255)

training_set = train_datagen.flow_from_directory('dataset/training_set', target_size=(64, 64), batch_size=32,
                                                 class_mode='categorical')

test_set = test_datagen.flow_from_directory('dataset/test_set', target_size=(64, 64), batch_size=32,
                                            class_mode='categorical')

if os.path.isfile("weights-classifier-cnn.hdf5"):
    fname = "weights-classifier-cnn.hdf5"
    classifier.load_weights(fname)
    print(f"{listdir('dataset/test_set/')}")
    test_image = image.load_img('dataset/test_set/124044lpr.jpg', target_size=(64, 64))
    test_image = image.img_to_array(test_image)
    test_image = np.expand_dims(test_image, axis=0)
    result = classifier.predict(test_image, verbose='0')

    # training_set.class_indices
    print(result)

# if result[0][0] == 1:
#	prediction='dog'
#	print(prediction)
# else:
#	prediction='cat'
#	print(prediction)

# LOAD THE DATA EXACTLY HERE !!

else:

    classifier.fit_generator(training_set,
                             steps_per_epoch=8000,
                             epochs=2,
                             validation_data=test_set,
                             validation_steps=2000)

    fname = "weights-classifier-cnn.hdf5"
    classifier.save_weights(fname, overwrite=True)

# import numpy as numpy
# from keras.preprocessing import image
