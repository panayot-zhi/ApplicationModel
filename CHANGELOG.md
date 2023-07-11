# [v1.1.5] - 2023-7-11 [PR: #42](https://github.com/aksio-insurtech/ApplicationModel/pull/42)

### Fixed

- Taking out the use of `ModuleInitializer`, turns out the compiler warning about using it in a package was there for a good reason, it doesn't really work, at least not in production environment.


# [v1.1.4] - 2023-7-10 [PR: #41](https://github.com/aksio-insurtech/ApplicationModel/pull/41)

### Fixed

- Fixed null reference exception for duplicate `SendAsync` call caused by an error during merging.


# [v1.1.3] - 2023-7-10 [PR: #40](https://github.com/aksio-insurtech/ApplicationModel/pull/40)

### Fixed

- Improved performance related to MongoDB watching.
- Made watching more reliable, if it crashes it shouldn't take down the process with it.
- Removed quite a few memory leaks related to MongoDB watching.



# [v1.1.2] - 2023-6-20 [PR: #0]()

No release notes

# [v1.1.1] - 2023-6-20 [PR: #39](https://github.com/aksio-insurtech/ApplicationModel/pull/39)

### Fixed

- Upgraded Fundamentals


# [v1.1.0] - 2023-6-20 [PR: #38](https://github.com/aksio-insurtech/ApplicationModel/pull/38)

### Added

- Adding the possibility to exclude types by their namespace in the Autofac `SelfBindingRegistrationSource`.



# [v1.0.12] - 2023-6-16 [PR: #0]()

No release notes

# [v1.0.11] - 2023-6-14 [PR: #37](https://github.com/aksio-insurtech/ApplicationModel/pull/37)

### Fixed

- Adding a `ModuleInitializer` to perform all the exclusion of assemblies for the type discovery system.


# [v1.0.10] - 2023-6-14 [PR: #36](https://github.com/aksio-insurtech/ApplicationModel/pull/36)

### Fixed

- Upgrading to latest **Fundamentals** and leveraging its global instance of `Types` and `DerivedTypes`.


# [v1.0.9] - 2023-6-11 [PR: #0]()

No release notes

# [v1.0.8] - 2023-6-10 [PR: #0]()

No release notes

# [v1.0.7] - 2023-6-10 [PR: #0]()

No release notes

# [v1.0.6] - 2023-6-9 [PR: #0]()

No release notes

# [v1.0.5] - 2023-5-30 [PR: #31](https://github.com/aksio-insurtech/ApplicationModel/pull/31)

### Added

- Serilog project - separating out specific Serilog artifacts.

### Fixed

- How we use the Types system from Fundamentals after its API change.

# [v1.0.4] - 2023-5-25 [PR: #0]()

No release notes

# [v1.0.2] - 2023-5-25 [PR: #0]()

No release notes

# [v1.0.1] - 2023-5-25 [PR: #0]()

No release notes

# [v1.0.0] - 2023-5-25 [PR: #0]()

No release notes

# [v0.0.11] - 2023-5-24 [PR: #0]()

No release notes

# [v0.0.10] - 2023-5-24 [PR: #0]()

No release notes

# [v0.0.9] - 2023-5-24 [PR: #0]()

No release notes

# [v0.0.8] - 2023-5-24 [PR: #0]()

No release notes

# [v0.0.7] - 2023-5-23 [PR: #0]()

No release notes

# [v0.0.6] - 2023-5-23 [PR: #0]()

No release notes

# [v0.0.5] - 2023-5-23 [PR: #0]()

No release notes

# [v0.0.3] - 2023-5-23 [PR: #0]()

No release notes

# [v0.0.2] - 2023-5-23 [PR: #0]()

No release notes

# [v0.0.1] - 2023-5-23 [PR: #0]()

No release notes

