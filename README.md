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
      1. [1D Simple Tetrahedron](https://github.com/0shu/Final-Year-Project/new/master?readme=1#1d-simple-tetrahedron)
      1. [3D Simple Tetrahedron](https://github.com/0shu/Final-Year-Project/new/master?readme=1#3d-simple-tetrahedron)
1. [**Multi-Primitive Shapes**](https://github.com/0shu/Final-Year-Project/new/master?readme=1#multi-primitive-shapes)
<!--   1. [Triangular Bipyramid](https://github.com/0shu/Final-Year-Project/new/master?readme=1#triangular-bipyramid)
   1. [Tetrahedron Stip](https://github.com/0shu/Final-Year-Project/new/master?readme=1#tetrahedron-strip)
   1. [Cuboid of tetrahedra](https://github.com/0shu/Final-Year-Project/new/master?readme=1#cuboid-of-tetrahedra)
1. [**Shape Splitting**](https://github.com/0shu/Final-Year-Project/new/master?readme=1#shape-splitting)
   1. [Halfing a tetrahedron](https://github.com/0shu/Final-Year-Project/new/master?readme=1#halfing-a-tetrahedron)
   1. [Cuboid into tetrahedrons](https://github.com/0shu/Final-Year-Project/new/master?readme=1#cuboid-into-tetrahedrons-->
<br>
<br>

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
![In Progress](/Assets/simple-tetrahedron.gif)
<br><br><br>

#### 3D Simple Tetrahedron
Because of the way simple tetrahedrons work, height to the oppsing face is the only thing we care about for volume, so with another fucntion we can apply a 3D vector input force instead of just a boring scalar value. This means we can give a translation on a vertex sideways components, we do this by breaking the vector into its normal component and remainder, applying the normal then adding the remainder to the resultant vertex.
![In Progress](/Assets/In-progress.png)
<br><br><br>

## Multi-Primitive Shapes
Here's where I'll show the progress of chaining mutiple primitives together by connecting common vertices, edges, and faces.
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