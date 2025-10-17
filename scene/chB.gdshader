shader_type canvas_item;

// Параметр, задающий процент перезарядки (0 = готово, 1 = полностью в перезарядке)
uniform float recharge_progress : hint_range(0.0, 1.0) = 1.0;

void fragment() {
    vec4 color = texture(TEXTURE, UV);
    
    // Определяем, насколько пиксель попадает в "зону перезарядки"
    float gray_zone = 1.0 - recharge_progress; // снизу вверх
    
    if (UV.y < gray_zone) {
        // нижняя часть — цветная
        COLOR = color;
    } else {
        // верхняя часть — черно-белая (ещё перезаряжается)
        float gray = dot(color.rgb, vec3(0.299, 0.587, 0.114));
        COLOR = vec4(vec3(gray), color.a);
    }
}
