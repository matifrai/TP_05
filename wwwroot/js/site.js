document.addEventListener('DOMContentLoaded', function () {
    const form = document.getElementById('registroForm');

    if (!form) return;

    form.addEventListener('submit', function (e) {
        e.preventDefault();

        const nombre = document.getElementById('nombre').value.trim();
        const apellido = document.getElementById('apellido').value.trim();
        const usuario = document.getElementById('usuario').value.trim();
        const contrasena = document.getElementById('contrasena').value.trim();
        const tipoUsuario = document.getElementById('tipoUsuario').value;

        // limpiar errores
        document.getElementById('err-nombre').textContent = '';
        document.getElementById('err-apellido').textContent = '';
        document.getElementById('err-usuario').textContent = '';
        document.getElementById('err-contrasena').textContent = '';
        document.getElementById('err-tipoUsuario').textContent = '';

        let ok = true;

        if (nombre.length < 2) {
            document.getElementById('err-nombre').textContent = 'Ingrese un nombre válido.';
            ok = false;
        }

        if (apellido.length < 2) {
            document.getElementById('err-apellido').textContent = 'Ingrese un apellido válido.';
            ok = false;
        }

        if (usuario.length < 4) {
            document.getElementById('err-usuario').textContent = 'El usuario debe tener al menos 4 caracteres.';
            ok = false;
        }

        if (contrasena.length < 6) {
            document.getElementById('err-contrasena').textContent = 'La contraseña debe tener al menos 6 caracteres.';
            ok = false;
        }

        if (!tipoUsuario) {
            document.getElementById('err-tipoUsuario').textContent = 'Seleccione un tipo de usuario.';
            ok = false;
        }

        if (ok) {
            form.submit();
        }
    });
});