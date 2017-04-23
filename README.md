# MapMaker

[![AppVeyor Builds](https://ci.appveyor.com/api/projects/status/github/a1essandro/MapMaker?branch=master&svg=true)](https://ci.appveyor.com/project/a1essandro/MapMaker/build/artifacts)

## Algorithms

### Heightmaps

#### [Diamond-Square](https://en.wikipedia.org/wiki/Diamond-square_algorithm)

You can use class [DiamondSquare](Generators/DiamondSquare.cs) like in example:
```cs
byte sizePower = 3; //result size = pow(2, sizePower) + 1
float persistence = 1.1;
var config = new DiamondSquareConfig(sizePower, persistence /*optional*/);
var generator = new DiamondSquare(config);

float[,] a = generator.Generate();
```
