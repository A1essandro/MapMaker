# MapMaker

## Algorithms

### Heightmaps

#### [Diamond-Square](https://en.wikipedia.org/wiki/Diamond-square_algorithm)

You can use class [DiamondSquare](Generators/DiamondSquare.cs) like in example:
```
	var config = new DiamondSquareConfig(2);
	var generator = new DiamondSquare(config);

	float[,] a = generator.Generate();
```
