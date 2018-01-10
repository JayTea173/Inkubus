#version 450 core

layout (location = 0) in vec4 position;
out vec4 vs_color;

void main(void)
{
 gl_Position = position;
 frag_color = vec4(1.0, 0.0, 0.0, 0.0);
}