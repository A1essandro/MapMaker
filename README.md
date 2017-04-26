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

float[,] heights = generator.Generate();
```

You can also use an asynchronous call:

```cs
Task<float[,]> heightsTask = generator.GenerateAsync();

/// .................Any actions.................

float[,] heights = heightsTask.Result;
```


#### Noise
You can use class [Noise](Generators/Noise.cs):

```cs
byte size = 1000;
float persistence = 0.67; //best results between 0.5 and 0.8
var config = new NoiseConfig(sizePower, persistence);
var generator = new Noise(config);

float[,] heights = generator.Generate();
//or
Task<float[,]> heights = generator.GenerateAsync();
```