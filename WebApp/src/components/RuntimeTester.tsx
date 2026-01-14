import { createOrder } from "../api/runtime.api";

export const RuntimeTester = () => {
  const sendValid = async () => {
    const res = await createOrder({
      orderId: "ORD-001",
      amount: 199.99,
      isPaid: true,
    });
    console.log(res);
  };

  const sendInvalid = async () => {
    try {
      await createOrder({
        // @ts-expect-error test invalid payload
        amount: "WRONG_TYPE",
      });
    } catch (e) {
      console.error(e);
    }
  };

  return (
    <div>
      <h2>Runtime tester</h2>
      <button onClick={sendValid}>Send valid request</button>
      <button onClick={sendInvalid}>Send invalid request</button>
    </div>
  );
};