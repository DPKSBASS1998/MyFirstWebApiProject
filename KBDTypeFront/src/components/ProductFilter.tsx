// src/components/Product/ProductFilters.tsx
import React from "react";
import type { SwitchFilterDto } from "../dto/SwitchFilterDto";

type CatalogFiltersProps = {
  filter: SwitchFilterDto & { [key: string]: any };
  onChange: (e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>) => void;
  onSubmit: (e: React.FormEvent<HTMLFormElement>) => void;
  minValues: Record<string, number>;
  maxValues: Record<string, number>;
};

/**
 * CatalogFilters — компонент, який відображає блок фільтрів
 */
export default function CatalogFilters({
  filter,
  onChange,
  onSubmit,
  minValues,
  maxValues,
}: CatalogFiltersProps) {
  // Допоміжні локальні стани для відкриття/закриття секцій
  const [showForce, setShowForce] = React.useState(false);
  const [showTravel, setShowTravel] = React.useState(false);
  const [showPreTravel, setShowPreTravel] = React.useState(false);
  const [showTactilePos, setShowTactilePos] = React.useState(false);
  const [showTactileForce, setShowTactileForce] = React.useState(false);
  const [showPinCount, setShowPinCount] = React.useState(false);
  const [showPrice, setShowPrice] = React.useState(false);

  return (
    <form className="catalog-form" onSubmit={onSubmit}>
      {/* Тип */}
      <div className="catalog-block">
        <span>Тип:</span>
        <select
          name="type"
          className="catalog-input"
          value={filter.type ?? ""}
          onChange={onChange}
        >
          <option value="">будь-який</option>
          <option value="Linear">Linear</option>
          <option value="Tactile">Tactile</option>
          <option value="Clicky">Clicky</option>
        </select>
      </div>

      {/* Виробник */}
      <div className="catalog-block">
        <span>Виробник:</span>
        <input
          name="manufacturer"
          className="catalog-input"
          value={filter.manufacturer ?? ""}
          onChange={onChange}
          placeholder="будь-який"
        />
      </div>

      {/* Сила (gf) */}
      <div className="catalog-block">
        <div
          className="catalog-toggle"
          onClick={() => setShowForce((v) => !v)}
        >
          <span>Сила (gf){showForce ? "" : " від/до"}</span>
          <button type="button">
            {showForce ? (
              <i className="bi bi-chevron-up"></i>
            ) : (
              <i className="bi bi-chevron-down"></i>
            )}
          </button>
        </div>
        {showForce && (
          <>
            <input
              name="minOperatingForce"
              type="number"
              className="catalog-input"
              value={filter.minOperatingForce ?? ""}
              onChange={onChange}
              placeholder={`${minValues.operatingForce} gf`}
            />
            <input
              name="maxOperatingForce"
              type="number"
              className="catalog-input"
              value={filter.maxOperatingForce ?? ""}
              onChange={onChange}
              placeholder={`${maxValues.operatingForce} gf`}
            />
          </>
        )}
      </div>

      {/* Хід (мм) */}
      <div className="catalog-block">
        <div
          className="catalog-toggle"
          onClick={() => setShowTravel((v) => !v)}
        >
          <span>Хід (мм){showTravel ? "" : " від/до"}</span>
          <button type="button">
            {showTravel ? (
              <i className="bi bi-chevron-up"></i>
            ) : (
              <i className="bi bi-chevron-down"></i>
            )}
          </button>
        </div>
        {showTravel && (
          <>
            <input
              name="minTotalTravel"
              type="number"
              step="0.1"
              className="catalog-input"
              value={filter.minTotalTravel ?? ""}
              onChange={onChange}
              placeholder={`${minValues.totalTravel.toFixed(1)} mm`}
            />
            <input
              name="maxTotalTravel"
              type="number"
              step="0.1"
              className="catalog-input"
              value={filter.maxTotalTravel ?? ""}
              onChange={onChange}
              placeholder={`${maxValues.totalTravel.toFixed(1)} mm`}
            />
          </>
        )}
      </div>

      {/* Попередній хід (мм) */}
      <div className="catalog-block">
        <div
          className="catalog-toggle"
          onClick={() => setShowPreTravel((v) => !v)}
        >
          <span>Попередній хід (мм){showPreTravel ? "" : " від/до"}</span>
          <button type="button">
            {showPreTravel ? (
              <i className="bi bi-chevron-up"></i>
            ) : (
              <i className="bi bi-chevron-down"></i>
            )}
          </button>
        </div>
        {showPreTravel && (
          <>
            <input
              name="minPreTravel"
              type="number"
              step="0.1"
              className="catalog-input"
              value={filter.minPreTravel ?? ""}
              onChange={onChange}
              placeholder={`${minValues.preTravel.toFixed(1)} mm`}
            />
            <input
              name="maxPreTravel"
              type="number"
              step="0.1"
              className="catalog-input"
              value={filter.maxPreTravel ?? ""}
              onChange={onChange}
              placeholder={`${maxValues.preTravel.toFixed(1)} mm`}
            />
          </>
        )}
      </div>

      {/* Положення тактильного спрацьовування (мм) */}
      <div className="catalog-block">
        <div
          className="catalog-toggle"
          onClick={() => setShowTactilePos((v) => !v)}
        >
          <span>
            Положення спрацьовування (мм)
            {showTactilePos ? "" : " від/до"}
          </span>
          <button type="button">
            {showTactilePos ? (
              <i className="bi bi-chevron-up"></i>
            ) : (
              <i className="bi bi-chevron-down"></i>
            )}
          </button>
        </div>
        {showTactilePos && (
          <>
            <input
              name="minTactilePosition"
              type="number"
              step="0.1"
              className="catalog-input"
              value={filter.minTactilePosition ?? ""}
              onChange={onChange}
              placeholder={`${minValues.tactilePos.toFixed(1)} mm`}
            />
            <input
              name="maxTactilePosition"
              type="number"
              step="0.1"
              className="catalog-input"
              value={filter.maxTactilePosition ?? ""}
              onChange={onChange}
              placeholder={`${maxValues.tactilePos.toFixed(1)} mm`}
            />
          </>
        )}
      </div>

      {/* Сила тактильного спрацьовування (gf) */}
      <div className="catalog-block">
        <div
          className="catalog-toggle"
          onClick={() => setShowTactileForce((v) => !v)}
        >
          <span>
            Сила тактильного спрацьовування (gf)
            {showTactileForce ? "" : " від/до"}
          </span>
          <button type="button">
            {showTactileForce ? (
              <i className="bi bi-chevron-up"></i>
            ) : (
              <i className="bi bi-chevron-down"></i>
            )}
          </button>
        </div>
        {showTactileForce && (
          <>
            <input
              name="minTactileForce"
              type="number"
              className="catalog-input"
              value={filter.minTactileForce ?? ""}
              onChange={onChange}
              placeholder={`${minValues.tactileForce} gf`}
            />
            <input
              name="maxTactileForce"
              type="number"
              className="catalog-input"
              value={filter.maxTactileForce ?? ""}
              onChange={onChange}
              placeholder={`${maxValues.tactileForce} gf`}
            />
          </>
        )}
      </div>

      {/* Кількість пінів */}
      <div className="catalog-block">
        <div
          className="catalog-toggle"
          onClick={() => setShowPinCount((v) => !v)}
        >
          <span>Кількість пінів{showPinCount ? "" : " від/до"}</span>
          <button type="button">
            {showPinCount ? (
              <i className="bi bi-chevron-up"></i>
            ) : (
              <i className="bi bi-chevron-down"></i>
            )}
          </button>
        </div>
        {showPinCount && (
          <>
            <input
              name="pinCount"
              type="number"
              className="catalog-input"
              value={filter.pinCount ?? ""}
              onChange={onChange}
              placeholder={`${minValues.pinCount}`}
            />
          </>
        )}
      </div>

      {/* Ціна */}
      <div className="catalog-block">
        <div
          className="catalog-toggle"
          onClick={() => setShowPrice((v) => !v)}
        >
          <span>Ціна, ₴{showPrice ? "" : " від/до"}</span>
          <button type="button">
            {showPrice ? (
              <i className="bi bi-chevron-up"></i>
            ) : (
              <i className="bi bi-chevron-down"></i>
            )}
          </button>
        </div>
        {showPrice && (
          <>
            <input
              name="minPrice"
              type="number"
              step="0.01"
              className="catalog-input"
              value={filter.minPrice ?? ""}
              onChange={onChange}
              placeholder={`${minValues.price.toFixed(2)} ₴`}
            />
            <input
              name="maxPrice"
              type="number"
              step="0.01"
              className="catalog-input"
              value={filter.maxPrice ?? ""}
              onChange={onChange}
              placeholder={`${maxValues.price.toFixed(2)} ₴`}
            />
          </>
        )}
      </div>

      <div className="catalog-block catalog-stock">
        <label className="catalog-label inline-label" htmlFor="inStock">
          В наявності
          <input
            id="inStock"
            name="inStock"
            type="checkbox"
            className="catalog-checkbox"
            checked={!!filter.inStock}
            onChange={onChange}
          />
        </label>
      </div>

      <button type="submit" className="catalog-button">
        Застосувати
      </button>
    </form>
  );
}
