// Validation and helpers for registration/login
document.addEventListener('DOMContentLoaded', function () {
	const form = document.getElementById('registroForm');
	if (form) attachRegistrationHandler(form);
});

function attachRegistrationHandler(form) {
	form.addEventListener('submit', async function (e) {
		e.preventDefault();
		clearErrors();

		const nombre = document.getElementById('nombre').value.trim();
		const apellido = document.getElementById('apellido').value.trim();
		const usuario = document.getElementById('usuario').value.trim();
		const contrasena = document.getElementById('contrasena').value;
		const tipoUsuario = document.getElementById('tipoUsuario').value;

		let valid = true;

		const nameRegex = /^[A-Za-zÁÉÍÓÚáéíóúÑñ'\-\s]+$/;
		if (!nombre || !nameRegex.test(nombre)) {
			showError('err-nombre', 'Nombre inválido (solo letras y espacios).');
			valid = false;
		}
		if (!apellido || !nameRegex.test(apellido)) {
			showError('err-apellido', 'Apellido inválido (solo letras y espacios).');
			valid = false;
		}

		const minUserLen = 4;
		if (!usuario || usuario.length < minUserLen) {
			showError('err-usuario', `Nombre de usuario debe tener al menos ${minUserLen} caracteres.`);
			valid = false;
		}

		const minPassLen = 6;
		if (!contrasena || contrasena.length < minPassLen) {
			showError('err-contrasena', `Contraseña debe tener al menos ${minPassLen} caracteres.`);
			valid = false;
		}

		if (!tipoUsuario) {
			showError('err-tipoUsuario', 'Seleccione un tipo de usuario.');
			valid = false;
		}

		if (!valid) return;

		// Check username availability via AJAX
		try {
			const resp = await fetch(`/Home/CheckUsername?usuario=${encodeURIComponent(usuario)}`);
			if (!resp.ok) throw new Error('Error verificando usuario');
			const data = await resp.json();
			if (!data.available) {
				showError('err-usuario', 'El nombre de usuario ya está en uso.');
				return;
			}
		} catch (err) {
			showError('err-usuario', 'No fue posible verificar el nombre de usuario. Intente más tarde.');
			return;
		}

		// All good -> submit
		form.submit();
	});
}

function showError(elementId, message) {
	const el = document.getElementById(elementId);
	if (el) {
		el.textContent = message;
		el.style.display = 'block';
		const inp = el.previousElementSibling;
		if (inp && inp.classList) inp.classList.add('is-invalid');
	}
}

function clearErrors() {
	['err-nombre','err-apellido','err-usuario','err-contrasena','err-tipoUsuario'].forEach(id=>{
		const el = document.getElementById(id);
		if (el) {
			el.textContent = '';
			el.style.display = 'none';
			const inp = el.previousElementSibling;
			if (inp && inp.classList) inp.classList.remove('is-invalid');
		}
	});
}
