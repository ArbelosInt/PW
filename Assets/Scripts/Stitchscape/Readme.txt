Instructions for use:
---------------------

Stitchscape is written for Unity 3.4 and later.

Importing the Stitchscape package will result in a new menu item in the Terrain menu called "Stitch...". Note that all terrains must have the same heightmap resolution. Also, all terrains should be located at the same height (i.e., have the same value for the Y axis). They should be positioned on the X and Z axes so that there is no overlap or gaps between terrains. Stitchscape can only alter the terrain heightmaps, and doesn't make any other adjustments to the terrain objects.

To stitch terrains together, select the Stitch... menu item. This creates a window where you can set up the terrains that you want stitched. You can do this manually, or use the "Autofill from scene" button.

If you're doing it manually, the default setup is 2 terrains across by 2 terrains down. If necessary, change these numbers to match your setup, then click the "Apply" button. Note that "across" means arranged in the world X axis, and "down" means arranged in the world Z axis, as viewed from above when choosing "top" as the scene camera view. Next, arrange your terrains in the grid below. You can do this by either dragging terrain objects from the hierarchy view to the appropriate grid boxes in the Stitchscape window, or you can use the selector icons in the grid boxes to select terrains from a list of Terrain objects in the scene.

If you use the "Autofill from scene" button, Stitchscape will get a list of all terrains in the scene and attempt to order them into a grid. The terrains must be already set up as a grid in the scene, and positioned next to each other. Ideally they should match up exactly, so they look seamless after stitching, but autofill will still work if they are reasonably close (plus or minus one unit). It's not necessary to set the across and down fields when using autofill.

Note that if you use a terrain grid size of 1x1, then the single terrain will be made seamlessly repeatable with itself, so it can be tiled indefinitely.

Below the terrain grid is the "Stitch width" slider. This is the number of terrain pixels from the edges of each terrain that will be affected by the stitching. The larger the number, the wider the band that will be affected. You should use larger values for terrains that don't match very well, and small values for terrains that nearly match to begin with. The minimum stitch width is 2, and the maximum is half of the terrain resolution.

The "Clear" button will remove all terrains from the stitching grid (but doesn't affect the terrains themselves) in case you want to start over. When you are all set, click the "Stitch" button. This will alter the terrains so they match up seamlessly. If you want to undo this, the normal Unity undo functions will work as expected. Note that proper use of the Terrain.SetNeighbors function in your scripts is still required in order for terrain LOD to match up correctly at a distance.


© 2012 Starscene Software