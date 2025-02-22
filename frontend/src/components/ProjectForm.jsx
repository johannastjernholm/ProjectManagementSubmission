import { useState } from "react";

const ProjectForm = () => {
  const [formData, setFormData] = useState({
    description: "",
    notes: "",
    startDate: "",
    endDate: "",
    status: "",
    customerName: "",
    customerEmail: "",
  });

  //Hantera ändringar i inputfälten
  const handleChange = (e) => {
    setFormData({
      ...formData,
      [e.target.name]: e.target.value,
    });
  };

  // Hantera formuläret
  const handleSubmit = async (e) => {
    e.preventDefault();
    console.log("skickar till apiet:", formData);

    // Skicka data till web apiet med Post
    try {
      const response = await fetch("https://localhost:7118/api/project", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(formData),
      });

      if (response.ok) {
        console.log("Projekt tillagt!");

        setFormData({
          description: "",
          notes: "",
          startDate: "",
          endDate: "",
          status: "",
          customerEmail: "",
          customerName: "",
        });
      } else {
        console.log("Något gick fel!");
      }
    } catch (error) {
      console.error("Fel vid anrop till API:", error);
    }
  };
  return (
    <>
      <form onSubmit={handleSubmit}>
        <h2>Add project</h2>
        <input type="text" name="description" placeholder="Description" value={formData.description} onChange={handleChange} />
        <input type="text" name="notes" placeholder="Notes" value={formData.notes} onChange={handleChange} />
        <input type="date" name="startDate" value={formData.startDate} onChange={handleChange} required />
        <input type="date" name="endDate" value={formData.endDate} onChange={handleChange} />
        <input type="text" name="status" placeholder="Status" value={formData.status} onChange={handleChange} required />
        <input type="text" name="customerName" placeholder="Customer name" value={formData.customerName} onChange={handleChange} required />
        <input type="email" name="customerEmail" placeholder="Email" value={formData.customerEmail} onChange={handleChange} required />
        <br />
        <br />
        <button type="submit">Skapa projekt</button>
      </form>
      <br />
    </>
  );
};

export default ProjectForm;
