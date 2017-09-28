SYS3
====


Organization
------------

All relevant code for actually running things with the ENS should be contained
within the ``rampy`` package. Other stuff in the repository root directory is
generally either outdated or for testing. In the former case, files should be
purged to keep things clean.


Connecting the ENS
------------------

From discussions with Medtronic, there are some issues regarding Windows 7 and
how the ENS connects via USB. This is evidently due to increasing the number
of COM ports each time the ENS connects or is turned on/off. To avoid this
issue, use the following procedure:

1. Turn on the ENS
2. Connect the ENS to the computer via USB
3. Power cycle the ENS


Testing
-------

The ``rampy.test`` package contains unit and other automated tests. Tests are
run using pytest_. Ideally, tests would be run in a fresh environment every
time, but this is currently not easily implemented considering the need for
access to data on rhino. To manually create a conda environment, do something
like this::

    > conda env create --file=conda_env.yaml -n ramulator_test

and activate the ``ramulator_test`` environment.

Long-running tests that require access to rhino are marked with the
``@pytest.mark.rhino`` decorator. To run only quick tests::

    > python -m rampy.test -m "not rhino"

Alternatively, to *only* run these tests::

    > python -m rampy.test -m "rhino"

.. _pytest: https://docs.pytest.org/en/latest/contents.html


How to build and run from source on windows
-------------------------------------------
(As of 9/28/2017)

Acquire the following dependencies
	S3_CPP master branch,
	SYS3 master branch, 
	Visual Studio 2015 (from microsoft),
	OdinLib 2.0.0 (from ram),
	Git and add git to path (https://git-scm.com/download/win),
	Experiment config files (also from ram),
	Miniconda for python 2.7 and add Scripts directory to path (https://conda.io/miniconda.html)


Follow these steps in a console
go to SYS3 root directory
conda env create --file=conda_env.yaml -n ramulator
create a conda environment from the conda_env.yaml file and name it ramulator
activate ramulator
activate the conda enviroment
conda install swig
conda install cmake
go to S3_CPP root directory
python cmake.py -ac
build the C++ portion of ramulator.
-ac ensures that everything is built and old builds are deleted.
pip install git+https://github.com/pennmem/bptools.git
install the bp tools library from our github
go back to SYS3 directory
python ramulator_main.py
execute ramulator.
at this point you should be able to successfully load configuration files.


Deployment
----------

See the README in the ``installer`` directory.
