export default function Highlight({ kingdomImg, kingdomName }) {
  return (
    <li>
      <img src={kingdomImg} alt={kingdomName} />
      <h3>{kingdomName}</h3>
    </li>
  );
}
