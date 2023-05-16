# SmartFloat Changelog

All notable changes to this package will be documented in this file.

The format is based on [Keep a Changelog](http://keepachangelog.com/en/1.0.0/)
and this project adheres to [Semantic Versioning](http://semver.org/spec/v2.0.0.html).


## [2.0.4] - 15-02-2023

### Changed
- Epsilon has been split into MinMaxEpsilon and ChangeEpsilon. ChangeEpsilon is only used for set comparisons, while MinMaxEpsilon is used for determining AtMin and AtMax.

## [1.2.0] - 21-04-2022

### Removed
- EasingNormal has been deprecated.
- Vanilla Easing and Type Menu are no longer dependencies.

## [1.1.0] - 09-03-2022

### Changed
- Support for Danger is now built into Normal and EasingNormal automatically. They will opt-in to use BitwiseEquals if Danger is detected.

### Removed
- UnsafeNormal has been deprecated.
- UnsafeEasingNormal has been deprecated.

## [1.0.0] - 06-06-2021

### Added
- Created package.

### Changed
- Nothing.

### Removed
- Nothing