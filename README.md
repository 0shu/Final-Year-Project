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
1. [**Simple Primitives**](https://github.com/0shu/Final-Year-Project/new/master?readme=1#simple-primitives---cuboid--tetrahedron)
   1. [Simple Cuboid](https://github.com/0shu/Final-Year-Project/new/master?readme=1#simple-cuboid)
   1. [Simple Tetrahedron](https://github.com/0shu/Final-Year-Project/new/master?readme=1#simple-tetrahedron)
1. [**Multi-Primitive Shapes**](https://github.com/0shu/Final-Year-Project/new/master?readme=1#multi-primitive-shapes)
   1. [Triangular Bipyramid](https://github.com/0shu/Final-Year-Project/new/master?readme=1#triangular-bipyramid)
   1. [Tetrahedron Stip](https://github.com/0shu/Final-Year-Project/new/master?readme=1#tetrahedron-strip)
   1. [Cuboid of tetrahedra](https://github.com/0shu/Final-Year-Project/new/master?readme=1#cuboid-of-tetrahedra)
1. [**Shape Splitting**](https://github.com/0shu/Final-Year-Project/new/master?readme=1#shape-splitting)
   1. [Halfing a tetrahedron](https://github.com/0shu/Final-Year-Project/new/master?readme=1#halfing-a-tetrahedron)
   1. [Cuboid into tetrahedrons](https://github.com/0shu/Final-Year-Project/new/master?readme=1#cuboid-into-tetrahedrons)
<br>
<br>

## Simple Primitives - *Cuboid & Tetrahedron*
The idea here was to start small and create something fast/light which can be strung together.
We can use lots of the same pieces to create a larger, more complex whole.
<br><br>

### Simple Cuboid
This shape is a staple of human thought. All its edges and faces are axis-aligned which makes working on it easier to comprehend and scaling it in different directions easier mathematically.<br>
![Simple Cuboid](/Assets/simple-cuboid.gif)
<br><br>

### Simple Tetrahedron
This Shape is the simples possible 3D shape, being made of the smallest number of the simplest possible 2D shape (4 Traingular Faces).
![In Progress](/Assets/In-progress.png)
<br><br><br>

## Multi-Primitive Shapes
Here's where I'll show the progress of chaining mutiple primitives together by connecting common vertices, edges, and faces.
![Not Started](/Assets/Not-started.png)
<br><br>

### Triangular Bipyramid
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
<br><br>