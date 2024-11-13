#include "include/raylib.h"
#include <stdio.h>

#define SCREEN_WIDTH 800
#define SCREEN_HEIGHT 500

int main(int argc, char** argv) {
    if (argc != 2) {
        puts("Usage: program <image_path>");
        return 1;
    }

    InitWindow(SCREEN_WIDTH, SCREEN_HEIGHT, "vga shader");

    Image image = LoadImage(argv[1]);
    SetWindowSize(image.width, image.height);
    Texture2D texture = LoadTextureFromImage(image);
    UnloadImage(image);

    Shader shader = LoadShader(NULL, "finaleffect.fs");
    Vector2 resolution = (Vector2){texture.width, texture.height};

    SetShaderValue(shader, GetShaderLocation(shader, "resolution"), &resolution, SHADER_UNIFORM_VEC2);

    SetTargetFPS(60);

    while (!WindowShouldClose()) {
        BeginDrawing();
        ClearBackground(BLACK);

        BeginShaderMode(shader);
        DrawTexture(texture, 0, 0, WHITE);
        EndShaderMode();

        EndDrawing();
    }

    UnloadTexture(texture);
    UnloadShader(shader);
    CloseWindow();

    return 0;
}