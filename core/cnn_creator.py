from keras.models import Sequential
from keras.layers import Conv2D
from keras.layers import MaxPooling2D
from keras.layers import Flatten
from keras.layers import Dense


class CnnCreator():

    def get_neural_network(self):
        classifier = Sequential()
        # Step 1 - Convolution
        classifier.add(Conv2D(32, (3, 3), input_shape=(24, 24, 3), activation='relu'))
        # Step 2 - Pooling
        classifier.add(MaxPooling2D(pool_size=(2, 2)))
        # Adding a second convolutional layer
        classifier.add(Conv2D(32, (3, 3), activation='relu'))
        classifier.add(MaxPooling2D(pool_size=(2, 2)))
        # Step 3 - Flattening
        classifier.add(Flatten())
        # Step 4 - Full connection
        classifier.add(Dense(units=128, activation='relu'))
        classifier.add(Dense(units=1, activation='sigmoid'))
        # Compiling the CNN
        classifier.compile(optimizer='adam', loss='binary_crossentropy', metrics=['accuracy'])
        return classifier
