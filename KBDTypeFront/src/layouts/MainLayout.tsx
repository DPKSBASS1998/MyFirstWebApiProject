// src/layouts/MainLayout.tsx
import React, { useState, useEffect } from "react";
import { Link, Outlet, useNavigate } from "react-router-dom";
import "../styles/Layout.css";

import CartModal from "../components/CartModal";
import AuthModal from "../components/AuthModal";
import WishlistModal from "../components/WishlistModal";

import { useIsAuthenticated, useUser, useLogout } from "../hooks/useAuth";
import { useCart } from "../hooks/useCart";
import { useWishlist } from "../hooks/useWishlist";

// Імпорт логотипу через модуль Vite (налаштуй vite-env.d.ts для .png)
import logo from "../assets/logo.png";

interface MainLayoutProps {
  // Можна передавати функції ззовні, якщо потрібно керувати модалками централізовано
  onOpenCart?: () => void;
  onOpenAuth?: () => void;
}

const MainLayout: React.FC<MainLayoutProps> = ({ onOpenCart, onOpenAuth }) => {
  const [cartOpen, setCartOpen] = useState<boolean>(false);
  const [authOpen, setAuthOpen] = useState<boolean>(false);
  const [wishlistOpen, setWishlistOpen] = useState(false);
  const [profileMenuOpen, setProfileMenuOpen] = useState(false);

  const isAuthenticated = useIsAuthenticated();
  const user = useUser();
  const logout = useLogout();
  const { items: cartItems } = useCart();
  const { items: wishlistItems } = useWishlist();
  const navigate = useNavigate();

  // Обробник для кнопки «Профіль»
  const handleProfileClick = () => {
    if (isAuthenticated) {
      setProfileMenuOpen((v) => !v);
    } else {
      // Якщо передали ззовні проп onOpenAuth, викликаємо його,
      // інакше — відкриваємо внутрішню модалку
      if (onOpenAuth) {
        onOpenAuth();
      } else {
        setAuthOpen(true);
      }
    }
  };

  // Обробник виходу
  const handleLogout = async () => {
    await logout();
    setProfileMenuOpen(false);
    navigate("/", { replace: true });
  };

  // Закриття меню при кліку поза ним
  useEffect(() => {
    if (!profileMenuOpen) return;
    const handleClick = (e: MouseEvent) => {
      const menu = document.getElementById("profile-menu-popover");
      if (menu && !menu.contains(e.target as Node)) {
        setProfileMenuOpen(false);
      }
    };
    document.addEventListener("mousedown", handleClick);
    return () => document.removeEventListener("mousedown", handleClick);
  }, [profileMenuOpen]);

  // Додаємо useEffect для реакції на зміну isAuthenticated
  useEffect(() => {
    if (authOpen && isAuthenticated) {
      setAuthOpen(false); // Закриваємо модалку після логіну
    }
  }, [isAuthenticated, authOpen]);

  return (
    <div className="layout">
      {/* Navbar */}
      <header className="navbar">
        <div className="navbar-inner">
          <div className="left-side">
            <div className="brand">
              <Link to="/">
                KBDType <img src={logo} alt="KBDType logo" />
              </Link>
            </div>
            <Link to="/catalog" className="nav-item">
              <i className="bi bi-shop-window"></i> Каталог
            </Link>
          </div>
          <div className="right-side">
            <Link
              to="/orders"
              className="icon-button"
              aria-label="Замовлення"
            >
              <i className="bi bi-card-list"></i>
            </Link>
            <Link
              to="/notifications"
              className="icon-button"
              aria-label="Сповіщення"
            >
              <i className="bi bi-bell"></i>
            </Link>
            <button
              className="icon-button"
              aria-label="Список бажаного"
              onClick={() => setWishlistOpen(true)}
            >
              <i 
                className={
                  wishlistItems.length > 0 ? "bi bi-bag-heart-fill" : "bi bi-bag-heart"
                }
              ></i>
            </button>
            <button
              className="icon-button"
              aria-label="Кошик"
              onClick={onOpenCart ? onOpenCart : () => setCartOpen(true)}
            >
              <i
                className={
                  cartItems.length > 0
                    ? "bi bi-cart-check-fill"
                    : "bi bi-cart"
                }
              ></i>
            </button>
            <div className="profile-menu-wrapper">
              <button
                className="icon-button"
                aria-label="Профіль"
                onClick={handleProfileClick}
              >
                {isAuthenticated ? (
                  <i className="bi bi-person-check"></i>
                ) : (
                  <i className="bi bi-person"></i>
                )}
              </button>
              {isAuthenticated && profileMenuOpen && (
                <div id="profile-menu-popover" className="profile-menu-popover">
                  <button
                    className="profile-menu-item"
                    onClick={() => {
                      setProfileMenuOpen(false);
                      navigate("/my-account");
                    }}
                  >
                    Мій профіль
                  </button>
                  <button
                    className="profile-menu-item logout"
                    onClick={handleLogout}
                  >
                    Вийти
                  </button>
                </div>
              )}
            </div>
          </div>
        </div>
      </header>

      {/* Main Content */}
      <main className="main-content" role="main">
        <Outlet />
      </main>

      {/* Footer тільки на Home */}
      {location.pathname === "/" && (
        <footer className="footer" role="contentinfo">
          <p>
            &copy; 2025 – KBDType 
            <img className="footerlogo" src={logo} alt="KBDType logo" />. Всі
            права захищені.
          </p>
        </footer>
      )}

      {/* Wishlist Modal */}
      {wishlistOpen && (
        <WishlistModal
          onClose={() => setWishlistOpen(false)}
        />
      )}

      {/* Cart Modal */}
      {cartOpen && (
        <CartModal
          onClose={() => {
            setCartOpen(false);
          }}
        />
      )}

      {/* Auth Modal */}
      {authOpen && (
        <AuthModal
          onClose={() => {
            setAuthOpen(false);
          }}
        />
      )}
    </div>
  );
};

export default MainLayout;
