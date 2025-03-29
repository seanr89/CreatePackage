# CreatePackage
test app to create a nuget based package and add to github for access?


## Git Tagging
```
git tag -a v1.0.0 -m "$(git log -1 --format='%ci' <commit-hash>) - Initial release" <commit-hash>
```