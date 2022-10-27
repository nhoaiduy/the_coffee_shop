var wrap = document.getElementsByClassName('wrap')[0];

function DisplayWrap(type) {
    wrap.style.display = 'flex';
    document.getElementsByTagName('body')[0].style.overflow = 'hidden';
    if (type == 'l') {
        document.getElementById('loaiSanPham').style.display = 'flex';
    } else {
        document.getElementById('sanPham').style.display = 'flex';
    }
}

function HideWrap() {
    wrap.style.display = 'none';
    document.getElementsByTagName('body')[0].style.overflow = 'visible';
    document.getElementById('loaiSanPham').style.display = 'none';
    document.getElementById('sanPham').style.display = 'none';
}