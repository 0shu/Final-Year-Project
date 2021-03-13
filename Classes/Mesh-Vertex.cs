
public class Mesh_Vertex
{
    public Mesh_Material m_material;
    public Vector m_position;
    public float m_temperature;

    public list<Mesh_Vertex> m_connected;
    public float m_affect = 0.0f;

    public void TransferForce(Vector force, float power) {
        m_affect += power - (/* Some amount calc through temp, hardness etc*/);

        list<pair<float, Mesh_Vertex>> valid;
        float totalAngle;

        foreach(vert in m_connected) {
            Vector diff = vert - m_position;
            float angle = Vector.angleBetween(force, diff);
            if(angle < m_material.transferAngleWindow) {
                valid.push_back(pair<angle, vert>);
                totalAngle += angle;
            }
        }

        foreach(vert in valid) {
            vert.second.TransferForce(force, m_affect * (vert.first / totalAngle));
        }
    }

    public void ResetAffect() {
        m_affect = 0.0f;
    }

}