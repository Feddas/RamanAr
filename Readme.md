# Goal

Navigate 3D mesh of a Raman Spectroscopy in AR.

![](https://github.com/Feddas/RamanAr/releases/download/0.0.1/Screenshot_RamanAr0.0.1.jpg)

# Usage

1. Install [RamanAr#.#.#.apk](https://github.com/Feddas/MarqMetrix/releases) on [Android device with ArCore support](https://developers.google.com/ar/discover/supported-devices).
2. After opening the app, phone should show camera view with reload button in the top left.
3. Move camera view around in good lighting until floor or table surface shows a tracked plane.
4. Tap inside the plane to place the Raman graph.
5. Tap or drag along the Raman graph to bring up information on that point in the graph.
6. Move the phone towards the mesh to zoom in.

# Details

Spectroscopy graph mesh is generated with perlin noise. A shader colors the mesh based on height.

The code could be modified to use Raman Spectoscopy data from MarqMetrix to generate a heightmap based 3D mesh.

# Resources

### AR foundation

- https://lnkd.in/g-Emv2c
- https://www.linkedin.com/feed/update/urn:li:activity:6693901835377479680/

# Repositories

Github: https://github.com/Feddas/RamanAr
