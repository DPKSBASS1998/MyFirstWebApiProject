// src/pages/Home.jsx
import React, { useState } from "react";
import "../styles/Home.css";

const slides = [
  "/assets/slide1.png",
  "/assets/slide2.png",
  "/assets/slide3.png",
];
const generalImg = "/assets/intro.png";
const typesImg = "/assets/types.png";
const mechImg = "/assets/mechanical.png";
const opticalImg = "/assets/optical.png";
const magImg = "/assets/magnetic.png";

export default function Home() {
  const [current, setCurrent] = useState(0);

  const prevSlide = () =>
    setCurrent((current - 1 + slides.length) % slides.length);
  const nextSlide = () => setCurrent((current + 1) % slides.length);

  const infoBlocks = [
    {
      id: 1,
      title: "Що таке механічний світч",
      img: generalImg,
      text: `Механічний світч — це індивідуальний перемикач під кожну клавішу,
плавно поєднує корпус, стем, пружину та контакти для чіткого відгуку.`,
    },
    {
      id: 2,
      title: "Порівняння типів: Linear vs Clicky",
      img: typesImg,
      text: `**Linear:** без тактильного удару, плавний хід.
**Clicky:** помітний клік та відчутний тактильний відгук.`,
    },
    {
      id: 3,
      title: "Принцип роботи механічного світча",
      img: mechImg,
      text: `При натисканні стем зсуває пружину, замикаючи контакти — генерується сигнал.`,
    },
    {
      id: 4,
      title: "Оптичні світчі",
      img: opticalImg,
      text: `Замість металевих контактів використовують інфрачервоне світло:
переривання променя створює сигнал — високошвидкісний та довговічний.`,
    },
    {
      id: 5,
      title: "Магнітні світчі",
      img: magImg,
      text: `Холлові датчики чи магнітне поле виявляють рух стема без фізичного зносу —
екстремальна надійність.`,
    },
  ];

  return (
    <div className="home-page">
      {/* Carousel */}
      <div className="home-intro">
        <h1>Вітаємо в KBDType</h1>
        <p>Магазин найкращих механічних світчів</p>
      </div>

      <div className="carousel">
        {slides.map((src, i) => (
          <div key={i} className={`slide ${i === current ? "active" : ""}`}>
            <img src={src} alt={`Слайд ${i + 1}`} className="slide-image" />
          </div>
        ))}
        <button className="carousel-btn prev" onClick={prevSlide}>
          ‹
        </button>
        <button className="carousel-btn next" onClick={nextSlide}>
          ›
        </button>
      </div>

      {/* Інформаційні блоки */}
      <div className="switch-info">
        {infoBlocks.map(({ id, title, img, text }) => (
          <div key={id} className="info-block">
            <div className="info-img">
              <img src={img} alt={title} />
            </div>
            <div className="info-text">
              <h2>{title}</h2>
              <p>{text}</p>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
}
