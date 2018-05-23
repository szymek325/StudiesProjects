import functools
import logging


def exception(function):
    """
    A decorator that wraps the passed in function and logs 
    exceptions should one occur
    """

    @functools.wraps(function)
    def wrapper(*args, **kwargs):
        try:
            return function(*args, **kwargs)
        except:
            # log the exception
            err = "There was an exception in  "
            err += function.__name__
            logger = logging.getLogger("FaceRecognitionLogger")
            logger.exception(err)

            # re-raise the exception
            raise

    return wrapper
