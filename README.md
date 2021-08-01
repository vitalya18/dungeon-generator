# Dungeon generator
Dungeon generator can create mazes of unlimited size. The maze consists of rooms and corridors that are interconnected in a chaotic manner, but
guarantee the absence of inaccessible rooms and corridors that are connected to only one room

Example of output with mode == 1

```
2 2 1 2 1 2 2 2 1 1 2 1 2 1   
1 2   1     2 2 1 1   1   1   
2 1 2 2     1     1 1 1 1     
1   2 1 1 1 2 1 1             
    1 1 2 1   1 1 1           
    1 2 1 1 1     2 2 1 1     
  2 2 1           1 1         
  1 1 2 1 1     1 2           
    1   2 2 1 1 1 1 1 2       
        1 1 1 1   1   1       
1 2 2 2 2   1 1 2 2 1 1       
  1   1 1 1     1     1       
1 2   1   1 1 1               
              1 2 1           
1 2 2 2 2 2 2 2    
```

# Installation
```
dotnet run
```

# Usage
Main function have two mode. First mode generate and output raw array, where 1 is room, and 2 is corridor
Second mode is simple example that use algorithm from first mode for generate and output maze with a small ward, where each object
(room and corridor) present from themselves two-dimensional array in 5x5 size

Inside the DungeonGenerator class there is a public field dungeonMap, which is a two-dimensional array of AreaType structures

```csharp
struct AreaType
{
  public int Type; // if 1 is room, is 2 is corridor
  public bool Reach; // internal field for more correct construction of the maze
}
```

```csharp
// width - width of array; height - height of array, procentRoom - procentRoom on each line
var dungeonGenerator = new DungeonGenerator(width, height, procentRoom = 40);

// Init dungeon by room and corridor. Inside the method, the array is filled with data from the dungeon
// and method return this array
dungeonGenerator.Init();

// Dev method to output generated array in init method
dungeonGenerator.Output();

// width - width of array; height - height of array, procentRoom - procentRoom on each line
var dungeonDrawer = new DungeonDrawer(width, height, procentRoom = 40);

// Pretty output for dungeon. For better using clear console before running
dungeonDrawer.Draw();
```

# Where to apply

For example, this algorithm can be used when writing console games using the C # language or for projects on the Unity game engine
