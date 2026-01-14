import { AdminEndpoints } from "./components/AdminEndpoints";
import { RuntimeTester } from "./components/RuntimeTester";

function App() {
  return (
    <div>
      <h1>ApiForge – frontend playground</h1>
      <AdminEndpoints />
      <hr />
      <RuntimeTester />
    </div>
  );
}

export default App;