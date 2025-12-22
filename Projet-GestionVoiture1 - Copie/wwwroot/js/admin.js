// Toggle Sidebar
document.getElementById('toggleSidebar')?.addEventListener('click', function () {
    document.querySelector('.sidebar').classList.toggle('open');
});

// Active nav item
document.querySelectorAll('.sidebar-nav .nav-item').forEach(item => {
    if (item.href === window.location.href) {
        item.classList.add('active');
    }
});