# Changelog

All notable changes to this project are documented in this file. The format follows
[Keep a Changelog](https://keepachangelog.com/en/1.1.0/).

## [Unreleased]

### Added

- Maintenance controller with ping, rotate and report verbs.
- Vendored NuGet feed under `nuget/` with Newtonsoft.Json, RestSharp, SixLabors.ImageSharp.Drawing and SharpZipLib.

### Changed

- Builds deterministically with warnings as errors.
- `tools/make-history.sh` reproduces the commit graph.

### Security

- 2026-02-06: Upgraded SharpZipLib from 1.3.2 to 1.3.3 (GHSA-m22m-h4rf-pwq3 / CVE-2021-32840, GHSA-2x7h-96h5-rq84 / CVE-2021-32841, GHSA-mm6g-mmq6-53ff / CVE-2021-32842: path traversal when extracting archives).
- 2026-02-17: Fixed command injection in `RotateLogs` (CWE-78); the log is rotated in place and no shell runs.
- 2026-03-05: Fixed path traversal in `ReadReport` (CWE-22); only the file name of the request is used.
