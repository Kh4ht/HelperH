// using UnityEngine;
// using UnityEngine.UI;
// using UnityEngine.EventSystems;
// using UnityEngine.Events;
// using DG.Tweening;
// using System;

// namespace KH
// {
//     [RequireComponent(typeof(Image), typeof(AudioSource), typeof(RectTransform))]
//     [RequireComponent(typeof(CanvasGroup))]
//     public class KHUIAnimator : MonoBehaviour,
//         IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
//     {
//         #region ENUMS ██████████████████████████████████████████████████████████████████████████████

//         public enum TransitionType { None, ColorTint }
//         public enum OpenCloseUIAnimationType
//         {
//             None,
//             Scaling,
//             Fading,
//             LeftSliding,
//             RightSliding,
//             TopSliding,
//             ButtomSliding
//         }

//         #endregion
//         #region INSPECTOR FIELDS █████████████████████████████████████████████████████████████████████████████

//         [Header("🎞 Sprite Frame Animation Setting")]
//         public bool enableFrameAnimationSettings = false;
//         public Sprite[] frames;
//         [Min(1)] public float frameRate = 12f;
//         public bool loop = true;
//         public bool playOnStart = false;


//         [Header("⚙️ Button Setting")]
//         public bool enableButtonSettings = false;
//         [Tooltip("check to disable the button")]
//         public UnityEvent onClick = new();


//         [Header("🎨 Animation Settings")]
//         public bool enableAnimationSettings = false;
//         public OpenCloseUIAnimationType openOrCloseAnimation = OpenCloseUIAnimationType.None;
//         public TransitionType transition = TransitionType.None;
//         public Color highlightedColor = new(0.96f, 0.96f, 0.96f, 1f),
//             pressedColor = new(0.78f, 0.78f, 0.78f, 1f);
//         public float fadeDuration = 0.1f;

//         [Header("Animate on click")]
//         public bool animateOnClick = false;
//         [Min(0)] public float clickScaleAmount = 1.1f, clickTweenDuration = 0.15f;
//         public Ease clickTweenEase = Ease.OutBack;


//         [Header("Animate on hover")]
//         public bool animateOnHover = false;

//         [Min(0)] public float hoverScaleAmount = 1.1f, hovertweenDuration = 0.15f;
//         public Ease hoverTweenEase = Ease.OutBack;


//         [Header("🎵 Sound Settings")]
//         public bool enableSoundSettings = false;
//         public AudioClip clickSound;
//         [Range(0f, 1f)] public float soundVolume = 0.8f;

//         #endregion
//         #region PRIVATE FIELDS ███████████████████████████████████████████████████████████████████████████████

//         private const float OPEN_CLOSE_ANIM_DURATION = 0.15f;
//         private Image _image;
//         private AudioSource _audioS;
//         private RectTransform _rectT;
//         private CanvasGroup _canvasGroup;
//         private int _currentFrame;
//         private float _timer;
//         private bool _playing;
//         private Vector3 _originalScale;
//         private Vector3 _originalPos;
//         private Color _originalColor;
//         private Sprite _originalSprite;

//         public Sprite OriginalSprite => _originalSprite;

//         #endregion
//         #region UNITY METHODS ████████████████████████████████████████████████████████████████████████████████

// #if UNITY_EDITOR
//     void Reset()
//     {
//         _image = GetComponent<Image>();
//         _audioS = GetComponent<AudioSource>();
//         _rectT = GetComponent<RectTransform>();
//         _canvasGroup = GetComponent<CanvasGroup>();

//         _audioS.playOnAwake = false;
//     }
// #endif

//         void Awake()
//         {
//             _image = GetComponent<Image>();
//             _audioS = GetComponent<AudioSource>();
//             _rectT = GetComponent<RectTransform>();
//             _canvasGroup = GetComponent<CanvasGroup>();

//             _originalScale = transform.localScale;
//             _originalPos = _rectT.anchoredPosition;
//             _originalColor = _image.color;
//             _originalSprite = _image.sprite;
//         }

//         void Start()
//         {
//             if (enableFrameAnimationSettings)
//             {
//                 if (playOnStart)
//                     UIPlayFrames();
//             }
//         }

//         void Update()
//         {
//             if (enableFrameAnimationSettings)
//                 HandleFrameAnimation();
//         }

//         #endregion
//         #region POINTER EVENTS ███████████████████████████████████████████████████████████████████████████████

//         public void OnPointerEnter(PointerEventData eventData)
//         {
//             if (enableButtonSettings)
//             {

//             }

//             if (enableAnimationSettings)
//             {
//                 if (transition == TransitionType.ColorTint)
//                     TweenColor(highlightedColor);

//                 if (animateOnHover)
//                     TweenScale(_originalScale * hoverScaleAmount, hovertweenDuration, hoverTweenEase);
//             }

//             if (enableSoundSettings)
//             {

//             }
//         }

//         public void OnPointerExit(PointerEventData eventData)
//         {
//             if (enableButtonSettings)
//             {

//             }

//             if (enableAnimationSettings)
//             {
//                 if (transition == TransitionType.ColorTint)
//                     TweenColor(_originalColor);

//                 if (animateOnHover)
//                     TweenScale(_originalScale, hovertweenDuration, hoverTweenEase);
//             }

//             if (enableSoundSettings)
//             {

//             }
//         }

//         public void OnPointerClick(PointerEventData eventData)
//         {
//             if (enableButtonSettings)
//             {
//                 onClick?.Invoke();
//             }

//             if (enableAnimationSettings)
//             {
//                 if (transition == TransitionType.ColorTint)
//                 {
//                     TweenColor(pressedColor, onComplete: () =>
//                         TweenColor(_originalColor));
//                 }

//                 if (animateOnClick)
//                 {
//                     PlayClickAnimation();
//                 }
//             }

//             if (enableSoundSettings)
//             {
//                 if (clickSound != null)
//                     _audioS.PlayOneShot(clickSound, soundVolume);
//                 else
//                     print($"{nameof(clickSound)} is null");
//             }
//         }

//         #endregion
//         #region METHODS ██████████████████████████████████████████████████████████████████████████████████████

//         private void HandleFrameAnimation()
//         {
//             if (!_playing || frames.Length <= 1) return;

//             _timer += Time.deltaTime;
//             if (_timer >= 1f / frameRate)
//             {
//                 _timer -= 1f / frameRate;
//                 _currentFrame = (_currentFrame + 1) % frames.Length;
//                 _image.sprite = frames[_currentFrame];
//                 if (!loop && _currentFrame == frames.Length - 1)
//                     _playing = false;
//             }
//         }

//         private void TweenScale(Vector3 target, float duration, Ease ease)
//         {
//             transform.DOKill();
//             transform.DOScale(target, duration).SetEase(ease);
//         }

//         private void TweenColor(Color targetColor, Action onComplete = null)
//         {
//             _image.DOKill();
//             _image
//                 .DOColor(targetColor, fadeDuration)
//                 .SetEase(Ease.Linear)
//                 .OnComplete(() => onComplete?.Invoke());
//             _image.color = targetColor;
//         }

//         private void PlayClickAnimation()
//         {
//             transform.DOKill();
//             transform.DOScale(_originalScale * clickScaleAmount, clickTweenDuration)
//                 .SetEase(clickTweenEase)
//                 .OnComplete(() =>
//                     transform.DOScale(_originalScale, clickTweenDuration).SetEase(clickTweenEase)
//                 );
//         }

//         #endregion
//         #region PUBLIC API ███████████████████████████████████████████████████████████████████████████████████

//         public void UIPlayFrames()
//         {
//             if (!enableFrameAnimationSettings) return;

//             if (frames.Length > 1)
//             {
//                 _playing = true;
//                 _currentFrame = 0;
//                 _image.sprite = frames[0];
//             }
//             else
//                 print($"{nameof(frames)}.Length must be greater that 1");
//         }

//         public void UIStopFrames() => _playing = false;
//         public void UIResetFrames() => _currentFrame = 0;

//         // ---------------- OPEN ----------------
//         public void UIOpen()
//         {
//             if (!enableAnimationSettings) return;

//             gameObject.SetActive(true);
//             _rectT.DOKill();
//             _canvasGroup.DOKill();

//             switch (openOrCloseAnimation)
//             {
//                 case OpenCloseUIAnimationType.Scaling:
//                     _rectT.localScale = Vector3.zero;
//                     _rectT.DOScale(1f, OPEN_CLOSE_ANIM_DURATION).SetEase(Ease.OutBack);
//                     break;

//                 case OpenCloseUIAnimationType.Fading:
//                     _canvasGroup.alpha = 0;
//                     _canvasGroup.DOFade(1f, OPEN_CLOSE_ANIM_DURATION);
//                     break;

//                 case OpenCloseUIAnimationType.LeftSliding:
//                     _rectT.anchoredPosition = new Vector2(-Screen.width, _originalPos.y);
//                     _rectT.DOAnchorPos(_originalPos, OPEN_CLOSE_ANIM_DURATION).SetEase(Ease.OutBack);
//                     break;

//                 case OpenCloseUIAnimationType.RightSliding:
//                     _rectT.anchoredPosition = new Vector2(Screen.width, _originalPos.y);
//                     _rectT.DOAnchorPos(_originalPos, OPEN_CLOSE_ANIM_DURATION).SetEase(Ease.OutBack);
//                     break;

//                 case OpenCloseUIAnimationType.TopSliding:
//                     _rectT.anchoredPosition = new Vector2(_originalPos.x, Screen.height);
//                     _rectT.DOAnchorPos(_originalPos, OPEN_CLOSE_ANIM_DURATION).SetEase(Ease.OutBack);
//                     break;

//                 case OpenCloseUIAnimationType.ButtomSliding:
//                     _rectT.anchoredPosition = new Vector2(_originalPos.x, -Screen.height);
//                     _rectT.DOAnchorPos(_originalPos, OPEN_CLOSE_ANIM_DURATION).SetEase(Ease.OutBack);
//                     break;
//             }
//         }

//         // ---------------- CLOSE ----------------
//         public void UIClose()
//         {
//             if (!enableAnimationSettings) return;

//             _rectT.DOKill();
//             _canvasGroup.DOKill();

//             switch (openOrCloseAnimation)
//             {
//                 case OpenCloseUIAnimationType.Scaling:
//                     _rectT.DOScale(0f, OPEN_CLOSE_ANIM_DURATION * 0.8f)
//                         .SetEase(Ease.InBack)
//                         .OnComplete(() => gameObject.SetActive(false));
//                     break;

//                 case OpenCloseUIAnimationType.Fading:
//                     _canvasGroup.DOFade(0f, OPEN_CLOSE_ANIM_DURATION * 0.6f)
//                         .OnComplete(() => gameObject.SetActive(false));
//                     break;

//                 case OpenCloseUIAnimationType.LeftSliding:
//                     _rectT.DOAnchorPos(new Vector2(-Screen.width, _originalPos.y), OPEN_CLOSE_ANIM_DURATION * 0.8f)
//                         .SetEase(Ease.InBack)
//                         .OnComplete(() => gameObject.SetActive(false));
//                     break;

//                 case OpenCloseUIAnimationType.RightSliding:
//                     _rectT.DOAnchorPos(new Vector2(Screen.width, _originalPos.y), OPEN_CLOSE_ANIM_DURATION * 0.8f)
//                         .SetEase(Ease.InBack)
//                         .OnComplete(() => gameObject.SetActive(false));
//                     break;

//                 case OpenCloseUIAnimationType.TopSliding:
//                     _rectT.DOAnchorPos(new Vector2(_originalPos.x, Screen.height), OPEN_CLOSE_ANIM_DURATION * 0.8f)
//                         .SetEase(Ease.InBack)
//                         .OnComplete(() => gameObject.SetActive(false));
//                     break;

//                 case OpenCloseUIAnimationType.ButtomSliding:
//                     _rectT.DOAnchorPos(new Vector2(_originalPos.x, -Screen.height), OPEN_CLOSE_ANIM_DURATION * 0.8f)
//                         .SetEase(Ease.InBack)
//                         .OnComplete(() => gameObject.SetActive(false));
//                     break;

//                 default:
//                     gameObject.SetActive(false);
//                     break;
//             }
//         }

//         #endregion
//     }
// }
