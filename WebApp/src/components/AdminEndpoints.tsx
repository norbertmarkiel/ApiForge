import { useEffect, useState } from "react";
import {
  getEndpoints,
  createEndpoint,
  deleteEndpoint,
} from "../api/adminEndpoints.api";
import type { AdminEndpoint } from "../types/endpoint";

export const AdminEndpoints = () => {
  const [endpoints, setEndpoints] = useState<AdminEndpoint[]>([]);

  useEffect(() => {
    getEndpoints().then(setEndpoints);
  }, []);

  const handleCreate = async () => {
    await createEndpoint({
      name: "Create order",
      description: "Creates new order",
      path: "/orders/create",
      status: "Enabled",
      protocols: 1,
      rateLimit: { requestsPerSecond: 100 },
      schema: {
        fields: [
          { name: "orderId", type: "String", isRequired: true },
          { name: "amount", type: "Number", isRequired: true },
        ],
      },
    });

    setEndpoints(await getEndpoints());
  };

  const handleDelete = async (id?: string) => {
    if (!id) return;
    await deleteEndpoint(id);
    setEndpoints(await getEndpoints());
  };

  return (
    <div>
      <h2>Admin Endpoints</h2>
      <button onClick={handleCreate}>Create example endpoint</button>

      <ul>
        {endpoints.map(e => (
          <li key={e.id}>
            {e.name} ({e.path})
            <button onClick={() => handleDelete(e.id)}>DELETE</button>
          </li>
        ))}
      </ul>
    </div>
  );
};