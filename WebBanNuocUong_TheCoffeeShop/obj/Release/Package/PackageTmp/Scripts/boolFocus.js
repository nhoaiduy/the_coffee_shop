function TenEvent() {
    var temp = document.getElementById("ten");
    if (temp.value=="") {
        temp.classList.add("errorClass");
        input.focus();
    } else {
        temp.classList.remove("errorClass");
    }
    if (temp.value.length > 50) {
        alert("Họ tên nhỏ hơn hoặc bằng 50 ký tự!")
    }
}
function SdtEvent() {
    var temp = document.getElementById("sdt");
    var reg = /^[0-9]+$/;
    if (reg.test(temp.value) == false) {
        temp.classList.add("errorClass");
        input.focus();
    } else {
        temp.classList.remove("errorClass");
    }
    if (temp.value.length > 12) {
        alert("Vui lòng kiểm tra số điện thoại")
    }
}

function DiaChiEvent() {
    var temp = document.getElementById("diachi");
    if (temp.value == "") {
        temp.classList.add("errorClass");
        input.focus();
    } else {
        temp.classList.remove("errorClass");
    }
    if (temp.value.length > 100) {
        alert("Địa chỉ nhỏ hơn hoặc bằng 100 ký tự!")
    }
}

function EmailEvent() {
    var temp = document.getElementById("email");
    var reg = /@/;
    if (reg.test(temp.value) == false) {
        temp.classList.add("errorClass");
        input.focus();
    } else {
        temp.classList.remove("errorClass");
    }
    if (temp.value.length > 50) {
        alert("Email nhỏ hơn hoặc bằng 50 ký tự!")
    }
}

function DiaChi2Event() {
    var temp = document.getElementById("diachi2");
    if (temp.value.length > 100) {
        alert("Địa chỉ nhỏ hơn hoặc bằng 100 ký tự!")
    }
}

function UsernameEvent() {
    var temp = document.getElementById("username");
    if (temp.value == "") {
        temp.classList.add("errorClass");
        input.focus();
    } else {
        temp.classList.remove("errorClass");
    }
    if (temp.value.length > 20) {
        alert("Tên đăng nhập nhỏ hơn hoặc bằng 20 ký tự!")
    }
}

function PasswordEvent() {
    var temp = document.getElementById("password");
    if (temp.value == "") {
        temp.classList.add("errorClass");
        input.focus();
    } else {
        temp.classList.remove("errorClass");
    }
}

function KiemTraTrong() {
    TenEvent();
    SdtEvent();
    DiaChiEvent();
    EmailEvent();
    UsernameEvent();
    PasswordEvent();
}
