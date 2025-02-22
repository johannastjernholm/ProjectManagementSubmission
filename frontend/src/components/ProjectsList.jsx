import { useState } from "react";

const ProjectsList = () => {
  const [projects, setProjects] = useState([]);
  const [editProject, setEditProject] = useState(null);
  const [updatedProject, setUpdatedProject] = useState({});

  const fetchProjects = async () => {
    try {
      const response = await fetch("https://localhost:7118/api/project");

      if (!response.ok) {
        throw new Error("Fel vid hämtning");
      }

      const data = await response.json();
      console.log(data);
      setProjects(data);
    } catch (error) {
      console.error("Fel vid hämtning av projekt: ", error);
    }
  };

  const updateProject = async (id) => {
    try {
      const response = await fetch(`https://localhost:7118/api/project/${id}`, {
        method: "PUT",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(updatedProject),
      });

      if (response.ok) {
        setProjects((prevProjects) => prevProjects.map((proj) => (proj.id === id ? { ...proj, ...updatedProject } : proj)));
        setEditProject(null);
      } else {
        console.log("Något gick fel vid uppdatering!");
      }
    } catch (error) {
      console.error("Fel vid uppdatering av projekt:", error);
    }
  };

  const handleChange = (e) => {
    setUpdatedProject({
      ...updatedProject,
      [e.target.name]: e.target.value,
    });
  };

  return (
    <div>
      <h2>Recieve a list of your projects</h2>

      <button onClick={fetchProjects}>Get all projects</button>

      {projects.length === 0 ? (
        <p>Currently no projects found</p>
      ) : (
        <ul>
          {projects.map((project) => (
            <li key={project.id}>
              {editProject === project.id ? (
                <>
                  <input type="text" name="description" value={updatedProject.description} onChange={handleChange} placeholder="Description" />
                  <input type="text" name="notes" value={updatedProject.notes} onChange={handleChange} placeholder="Notes" />
                  <input type="date" name="startDate" value={updatedProject.startDate} onChange={handleChange} />
                  <input type="date" name="endDate" value={updatedProject.endDate} onChange={handleChange} />
                  <input type="text" name="status" value={updatedProject.status} onChange={handleChange} placeholder="Status" />

                  <button onClick={() => updateProject(project.id)}>Save</button>
                  <button onClick={() => setEditProject(null)}>Cancel</button>
                </>
              ) : (
                <>
                  <strong>{project.description}</strong> - {project.status} <br />
                  Start date: {project.startDate} | End date: {project.endDate || "Not specified"} {}
                  <button
                    onClick={() => {
                      setEditProject(project.id);
                      setUpdatedProject(project);
                    }}>
                    Update
                  </button>
                  <br />
                  <br />
                </>
              )}
            </li>
          ))}
        </ul>
      )}
    </div>
  );
};

export default ProjectsList;
