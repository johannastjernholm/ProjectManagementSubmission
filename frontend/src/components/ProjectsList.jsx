import { useState } from "react";

const ProjectsList = () => {
  const [projects, setProjects] = useState([]);

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

  return (
    <div>
      <h2>Lista över alla projekt</h2>

      <button onClick={fetchProjects}>Hämta alla projekt</button>
      {projects.length === 0 ? (
        <p>Inga projekt tillgängliga.</p>
      ) : (
        <ul>
          {projects.map((project) => (
            <li key={project.id}>
              <strong>{project.description}</strong> - {project.status} <br />
              Start: {project.startDate} | Slut: {project.endDate || "Ej angivet"}
            </li>
          ))}
        </ul>
      )}
    </div>
  );
};

export default ProjectsList;
