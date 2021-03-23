# Virtual Blacksmithing
![Prototype Screenshot](/Assets/Cropped-Screenshot.png)
**Joshua Simons - University Final Year Project**<br>
*Borne of frustration at current game crafting systems and a desire to make something more interesting and more like actual crafting instead of a list based collectathon*
<br><br>
The idea of this project is to create simulated experience of blacksmithing.<br>
Maniuplating 3D solids plastically by simulating forces and adjusting meshes.<br>
Ideally resulting in a system which just *inputs and outputs a singular mesh*.
<br>
<br>
### Contents
1. [**Mesh Manipulation**](https://github.com/0shu/Final-Year-Project#mesh-manipulation)
   1. [Visual Mesh](https://github.com/0shu/Final-Year-Project#visual-mesh)
3. [**Simple Primitives**](https://github.com/0shu/Final-Year-Project#simple-primitives---cuboid--tetrahedron)
   1. [Simple Cuboid](https://github.com/0shu/Final-Year-Project#simple-cuboid)
   1. [Simple Tetrahedron](https://github.com/0shu/Final-Year-Project#simple-tetrahedron)
      1. [1D Simple Tetrahedron](https://github.com/0shu/Final-Year-Project#1d-simple-tetrahedron)
      1. [3D Simple Tetrahedron](https://github.com/0shu/Final-Year-Project#3d-simple-tetrahedron)
4. [**Multi-Primitive Shapes**](https://github.com/0shu/Final-Year-Project#multi-primitive-shapes)
<!--   1. [Triangular Bipyramid](https://github.com/0shu/Final-Year-Project#triangular-bipyramid)
   1. [Tetrahedron Stip](https://github.com/0shu/Final-Year-Project#tetrahedron-strip)
   1. [Cuboid of tetrahedra](https://github.com/0shu/Final-Year-Project#cuboid-of-tetrahedra)
1. [**Shape Splitting**](https://github.com/0shu/Final-Year-Project#shape-splitting)
   1. [Halfing a tetrahedron](https://github.com/0shu/Final-Year-Project#halfing-a-tetrahedron)
   1. [Cuboid into tetrahedrons](https://github.com/0shu/Final-Year-Project#cuboid-into-tetrahedrons-->
<br>
<br>

## Mesh Manipulation
This is the main part of the project. We want to be able to modify the mesh by applying local forces/transformations to it, whilst maintaining a constant volume. Ideally these local effects will approximate real world blacksmithing to a degree by using physical simulations.<br>
Working directly with the mesh is a very complex topic which is hard to grasp and difficult to directly develop upon, so first I've started implementing simpler primitive shapes that allow for mor limited trasnformations in the hopes to combine/expand upon those as the project goes along! (*See* [*Simple Primtives*](https://github.com/0shu/Final-Year-Project#simple-primitives---cuboid--tetrahedron) *below*.)<br>
![Mesh Manipulation](/Assets/In-progress.png)<br>*
<br><br>

### Visual Mesh
I've implemented a simple class to display and connect vertices. This class will have the associated variables/functions to be able to pass a force through the vertices.<br>
![Mesh Manipulation](/Assets/vertex-mesh.gif)<br>
Having an easy to see/understand model will help speed up development, and assist in communicating in the report at the end.
<br><br><br>

## Simple Primitives - *Cuboid & Tetrahedron*
The idea here was to start small and create something fast/light which can be strung together.
We can use lots of the same pieces to create a larger, more complex whole.
<br><br>

### Simple Cuboid
This shape is a staple of human thought. All its edges and faces are axis-aligned which makes working on it easier to comprehend and scaling it in different directions easier mathematically.<br>
![Simple Cuboid](/Assets/simple-cuboid.gif)<br>
In the gif above you can see how applying hits to a certain side of the cuboid will shrink it in that axis and expand the other axes to maintain volume. This algorithm is very fast and can be adjusted to shrink the cuboid by any percentage amount in one axis, even using values greater than one to pull a face and stretch out the cuboid in that axis instead.<br>
This is very rudimentary and does not give any ability to change the topology at all. But may still be a useful part of a larger simulation process when just starting out with cuboid billets, and is thus a welcome optimisation which very well may continue right into the finished product.
<br><br>

### Simple Tetrahedron
A tetrahedron is the simplest possible 3D shape, being made of the smallest number of the simplest possible 2D shape (4 Traingular Faces). In this variation we imagine the tetrahedron as a combination of a face and an opposing vertex.
When maintaining volume, the distance of the vertex to the face is inversely proportional to the size of the face. Increasing one by a factor of 2 means decreasing the other by a factor of 2.

#### 1D Simple Tetrahedron
For this version we only care about the vertex as a 1D line away from the plane and thus all transformations apply along that normal line. We apply a scalar force along that normal and expand transformations out around that.
![1D Tetrahedron](/Assets/simple-tetrahedron.gif)
<br><br><br>

#### 3D Simple Tetrahedron
Rather than a proportional 1 dimensional up-down approach this tetrahedron does things the other way around. The vertex is moved along a vector by a certain amount then the proportional difference in height to the opposing face is figured out and the face then expands or contracts to maintain volume.
![3D Tetrahedron](/Assets/3D-simple-tetrahedron.gif)
<br><br><br>

## Multi-Primitive Shapes
Here's where I'll show the progress of chaining mutiple primitives together by connecting common vertices, edges, and faces.<br>
It's important to break down larger more complex shapes into smaller, more manageable ones.
![Not Started](/Assets/Not-started.png)
<br><br>

<!-- ### Triangular Bipyramid
This shape is like 2 tetrahedra stuck together by sharing one face.
![Not Started](/Assets/Not-started.png)
<br><br>
### Tetrahedron Strip
Once we have 2 tetrahedra working in concert chaining them n number of times should be easy!
![Not Started](/Assets/Not-started.png)
<br><br>
### Cuboid of tetrahedra
A slightly more advanced shape. 4 Tetrahedra surrounding an inner tetrahedron.
![Not Started](/Assets/Not-started.png)
<br><br><br>
## Shape-Splitting
Now that we can make and use shapes of multiple primitives, we want to be able to break up simple larger shapes into a collection of smaller shapes.
![Not Started](/Assets/Not-started.png)
<br><br>
### Halfing a tetrahedron
Adding an extra point on the edge of 1 teatrhedron to turn it into 2!
![Not Started](/Assets/Not-started.png)
<br><br>
### Cuboid into tetrahedrons
Turning a cube into multiple tetrahedrons!
![Not Started](/Assets/Not-started.png)
<br><br>-->
