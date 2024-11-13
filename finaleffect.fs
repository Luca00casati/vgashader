#version 330

uniform sampler2D texture0;    // Input texture
uniform vec2 resolution;       // Resolution of the image

in vec2 fragTexCoord;
out vec4 fragColor;

const float pixelSize = 2.0;  // 2x pizel size
const float MIN_DISTANCE = 0.15; // Minimum distance threshold for color matching

// VGA color palette (16 colors)
const vec3 vgaColors[16] = vec3[](
    vec3(0.0, 0.0, 0.0),         // Black
    vec3(0.0, 0.0, 0.67),        // Blue
    vec3(0.0, 0.67, 0.0),        // Green
    vec3(0.0, 0.67, 0.67),       // Cyan
    vec3(0.67, 0.0, 0.0),        // Red
    vec3(0.67, 0.0, 0.67),       // Magenta
    vec3(0.67, 0.33, 0.0),       // Brown
    vec3(0.67, 0.67, 0.67),      // Light Gray
    vec3(0.33, 0.33, 0.33),      // Dark Gray
    vec3(0.33, 0.33, 1.0),       // Light Blue
    vec3(0.33, 1.0, 0.33),       // Light Green
    vec3(0.33, 1.0, 1.0),        // Light Cyan
    vec3(1.0, 0.33, 0.33),       // Light Red
    vec3(1.0, 0.33, 1.0),        // Light Magenta
    vec3(1.0, 1.0, 0.33),        // Yellow
    vec3(1.0, 1.0, 1.0)          // White
);

// Function to find the closest VGA color
vec3 FindClosestVGAColor(vec3 color) {
    float minDist = MIN_DISTANCE;
    vec3 closestColor = color;
    
    for (int i = 0; i < 16; i++) {
        float dist = distance(color, vgaColors[i]);
        if (dist < minDist) {
            minDist = dist;
            closestColor = vgaColors[i];
        }
    }
    return closestColor;
}

void main()
{
    vec2 coord = vec2(
    floor(fragTexCoord.x * resolution.x / pixelSize) * pixelSize / resolution.x,
    floor(fragTexCoord.y * resolution.y / pixelSize) * pixelSize / resolution.y);

    vec3 texColor = texture(texture0, coord).rgb;

    vec3 quantizedColor = FindClosestVGAColor(texColor);

    fragColor = vec4(quantizedColor, 1.0);
}
