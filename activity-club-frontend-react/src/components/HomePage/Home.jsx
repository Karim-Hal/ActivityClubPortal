import Highlight from "./Highlight";
import Feedback from "./Feedback";
import fireKingomImg from "../../../public/images/fire-kingdom.jpg";
import candyKingdomImg from "../../../public/images/candy-kingdom.jpg";
import iceKingdomImg from "../../../public/images/ice-kingdom.jpg";
export default function Home({ memberEmail, handleExploreClick }) {
  return (
    <>
      <div id="home-hero">
        <div id="home-hero-content">
          <h2>Explore adventures!</h2>
          <p>
            Welcome to Adventure Time, where you will have the chance to explore
            a variety of kingdoms, their activities, and the myserious lore
            behind every town! Register for an event to secure your seat in this
            wild journey!
          </p>
          <button onClick={handleExploreClick}>Explore now!</button>
        </div>
      </div>
      <div id="home-highlights">
        <h2>Highlights</h2>
        <ul>
          <Highlight kingdomImg={fireKingomImg} kingdomName={"Fire Kingdom"} />
          <Highlight kingdomImg={iceKingdomImg} kingdomName={"Ice Kingdom"} />
          <Highlight
            kingdomImg={candyKingdomImg}
            kingdomName={"Candy Kingdom"}
          />
        </ul>
      </div>

      <div id="feedback-container">
        <Feedback memberEmail={memberEmail} />
      </div>
    </>
  );
}
