
public class Mesh_Vertex
{
    public Mesh_Material m_material;
    public Vector m_position;
    public float m_temperature;

    public list<Mesh_Vertex> m_connected;
    public float m_affect = 0.0f;

    public void TransferForce(Vector force, float power) {
        //Subtract temp absorbtion
        m_affect += power - (/* Some amount calc through temp, hardness etc*/);

        //Check vertex is not next to immovable and force is in direction of said immovable

        //Go through conected points and tranfer force where appropriate
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

    public void Compress(Vector normal, Vector center) {
        Vector direction; //Check if above or below center with relation to normal
        float compression = m_affect * m_material.hardness;
        direction *= compression;
        m_position += direction;
    }

    public void ResetAffect() {
        m_affect = 0.0f;
    }

}