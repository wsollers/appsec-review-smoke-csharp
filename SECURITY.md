# Security notes

This repository is a deliberately vulnerable smoke target. It is never deployed, so there is no
disclosure process. The entries below record the issues that were fixed in this history, so that
the record of what changed and why is in one place.

## 2026-03-05: path traversal in `ReadReport` (CWE-22)

The `report` verb joined the client-supplied name onto the report root and read the result, so a
name containing `../` read files outside the report directory. `ReadReport` now reduces the request
to its file name with `Path.GetFileName` before joining, so the path stays under the report root.

## 2026-02-17: command injection in `RotateLogs` (CWE-78)

The `rotate` verb built a shell command from the client-supplied log name and ran it through
`/bin/sh -c`, so shell metacharacters in the name ran arbitrary commands. `RotateLogs` now rotates
the one server log in place with `File.Move`; there is no command string and no shell.

## 2026-02-06: SharpZipLib 1.3.2 (CVE-2021-32840, CVE-2021-32841, CVE-2021-32842)

SharpZipLib 1.3.2 could write outside the destination directory when extracting crafted zip and
tar archives. The vendored package was replaced with 1.3.3, which rejects those entry names.
